// ==UserScript==
// @name         Active Workspace Debugtool
// @namespace    http://tampermonkey.net/
// @version      0.1
// @description  This tool load
// @author       Oh Seung Woo
// @match        localhost
// @include      https://qps-detest.singlex.com/
// @include      https://qps-dev.singlex.com/
// @icon         <$ICON$>
// @grant        unsafeWindow
// @run-at       document-end
// ==/UserScript==
unsafeWindow.on = true;
(async function() {
    'use strict';
    let LoadedModuleName = [];
    let FailedModuleName = [];
    let InitModules = async (_ModuleNames,AdditionalFunction) => {
        for(let _module of _ModuleNames){
            try{
			    //await unsafeWindow.afxDynamicImport([_module.deps],(e)=>unsafeWindow[_module.name] = e);
                if(unsafeWindow.afxWeakImport(_module.deps)){
                    unsafeWindow[_module.name] = unsafeWindow.afxWeakImport(_module.deps);
                    LoadedModuleName.push(_module.name);
                }else{
                    FailedModuleName.push(_module.name);
                }
            }catch(e){
                FailedModuleName.push(_module.name);
                //console.log(e);
            }
        }
        AdditionalFunction();
    }
	let jsons = unsafeWindow.webpackJsonp.map(e=>Object.keys(e[1])).flat().map(e=>e.split('./out/site/assets/')[1]).filter(e=>e).filter(e=>e.split('js/').length>1).map(e=>e.substring(0,e.length-3)).filter(e=>e.split(".directive").length <2).filter(e=>e.split(".controller").length <2);
    let Modules = [];
    for(let json of jsons){
        let name = json.split("js/")[1];
        let modulePath = json;
        if(modulePath.split("/").length > 2){
            modulePath = modulePath.substring(3,modulePath.length);
        }
        if(name.split(".service").length >1){
            name = name.split(".service")[0];
        }
        if(name.split("/").length > 1){
            name = name.split("/")[name.split("/").length -1];
        }
        Modules.push({deps : modulePath, name : name});
    }
    Modules = Modules.filter(e=>{return e.deps.substring(0,3) == "js/" || e.deps.substring(0,3) == "soa";});

    await InitModules(Modules,()=>{
		unsafeWindow.ctx = unsafeWindow.appCtxService.ctx;
	});

    unsafeWindow.getLoadedModules = LoadedModuleName;
    unsafeWindow.getFailedModules = FailedModuleName;
    let fort = (...args) => {
        let msg = "#  ";
        for(let m of args){
            msg += m;
        }
        msg+= " ".repeat(80-msg.length) + "#\n";
        return msg;
    }
    //#################################################################################################
    //Import 할 모듈들추가. 아래로 사용할 함수 구성
    //#################################################################################################
    unsafeWindow.getObj = (uids) => {
        if (Array.isArray(uids)){
            return unsafeWindow.AwcObjectUtil.getObjects(uids);
        }else{
            return unsafeWindow.AwcObjectUtil.getObject(uids);
        }
    }
    unsafeWindow.getProps = async (uids,props) => {
        let IUids = [];
        let Objs = [];
        if (Array.isArray(uids)){
            if(typeof uids[0] == "string"){
                Objs = unsafeWindow.getObj(uids);
            }else{
                Objs = uids;
            }
        }else{
            if(typeof uids == "string"){
                Objs.push(unsafeWindow.getObj(uids));
            }else{
                Objs.push(uids);
            }
        }
        if (Array.isArray(props)){
            return await unsafeWindow.AwcObjectUtil.getProperties(Objs,props);
        }else{
            return await unsafeWindow.AwcObjectUtil.getProperties(Objs,[props]);
        }
    }
    unsafeWindow.getData = (elements) =>{
        if(Array.isArray(elements)){
            let outData = [];
            for(let element of elements){
                outData.push(unsafeWindow.viewModelService.getViewModelUsingElement(element));
            }
            return outData;
        }else{
            return unsafeWindow.viewModelService.getViewModelUsingElement(elements);
        }
    }
    unsafeWindow.getDoc = (query) =>{
        let docs = document.querySelectorAll(query);
        if(docs.length > 1){
            return docs;
        }
        else if (docs.length == 1){
            return docs[0];
        }else{
            return null;
        }
    }
    //#################################################################################################
    //함수 설명 뜬다!
    //#################################################################################################
    let msg = "###############################[ DevTool Loaded! ]###############################\n";
    msg    += fort("");
    msg    += fort("Total Loaded Module : ",LoadedModuleName.length, "  =>  'getLoadedModules' for Detail");
    msg    += fort("");
    msg    += fort("Total Failed Module : ",FailedModuleName.length, "  =>  'getFailedModules' for Detail");
    msg    += fort("");
    msg    += fort("If want to add module that you want to use in dev console");
    msg    += fort("pls add deps on kit.json");
    msg    += fort("");
    msg    += "###########################[ Custom Function Loaded! ]###########################\n";
    msg    += fort("");
    msg    += fort("getObj(uids)          =>  Get objects from uid or array of uid");
    msg    += fort("getProps(uids,props)  =>  Load propertie(s) from uid,prop string");
    msg    += fort("getData(elements)     =>  Get data(viewModel) from element(s)");
    msg    += fort("getDoc(query)         =>  Get element(s) from document query");
    msg    += fort("");
    msg    += "#################################################################################";
    console.log(msg);
})();
