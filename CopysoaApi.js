function CreateButton(doc){
	doc.innerHTML = doc.innerHTML.substring(0,8) +"\t<button class=\"copyRequestBtn\" style=\"color:#006487;font-size:10px;border:2px;padding:1px;background-color:#81ed85;cursor:pointer;\">Copy Soa Request Param</button>" + doc.innerHTML.substring(8)
	let btn = document.getElementsByClassName("copyRequestBtn")[0];
	btn.onclick = onButton;
}

document.addEventListener('DOMContentLoaded', function (e) {
	let doc = document.getElementById('opRequest');
	CreateButton(doc);
	let scope = angular.element(doc).scope();
	scope.$watch('selectedOperation',function(e) { 
		CreateButton(doc);
	});
}, false);

function createIndentTab(level){
	let out = "";
	for(let i =0; i < level; i++){
		out+="\t";
	}
	return out;
}

function onButton(){
	for(let element of document.getElementsByClassName("keyInfoDiv")){
		element.style.display='none';
	}
	let doc = document.getElementById('opRequest');
	let copiedText = "";
	let isStartLine = true;
	let indentLevel = 0;
	for (let line of doc.outerText.split("\n")){
		if(isStartLine){
			isStartLine=false;
			copiedText += "let soaInputParam = "
		}else{
			let scop = 0;
			if(line == "{"||line == "[{"){
				indentLevel++;
				scop++;
			}else if(line == "}"||line == "}]"||line == "},"||line == "}],"){
				indentLevel--;
			}
			if(line.trim() != ""){
				let t = line;
				if(t.split("\"").length>1){
					let dataType = t.split("\"")[1];
					if(dataType.substring(dataType.length-2) == "[]"){
						dataType = "[" + dataType.substring(0,dataType.length-2)+"]";
					}
					t = t.split("\"")[0] + dataType;
				}
				copiedText += createIndentTab(indentLevel-scop)+t +"\n";
			}
		}
	}
	let isValue = (instarr,i) =>{
		let valueSplit = instarr[i].split(":");
		if (valueSplit.length > 0 ){
			if(instarr[i].indexOf(":")>-1){
				return true;
			}
		}
		return false;
	}
	let instarr = copiedText.split("\n");
	for(let i in instarr){
		if(i > 0){
			if(isValue(instarr,i)&& isValue(instarr,i-1)){
				instarr[i-1] += ","
			}
		}
	}
	copiedText = instarr.join("\n");
	let urldoc = document.getElementById('opUrl');
	let url = urldoc.outerText.split(":")[1].split("/");
	copiedText+="let response = await soaService.post(\"" + url[0].trim() + "\", \"" + url[1] + "\",soaInputParam);";
	navigator.clipboard.writeText(copiedText);
	console.log(copiedText);
	//document.getElementsByClassName("keyInfoDiv")[1].style.display='none';
}
