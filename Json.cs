string ByteFormat(int b)
{
  int level = 0;
  double dat = b;
  while(dat / 1024.0 > 1)
  {
    dat /= 1024.0;
    level++;
  }
  string o = dat.ToString("0.##");
  switch (level)
  {
    case 0:
      o += " Bytes";
      break;
    case 1:
      o += " Kb";
      break;
    case 2:
      o += " Mb";
      break;
    case 3:
      o += " Gb";
      break;
  }
  return o;
}

string input = "";
Stopwatch sw = Stopwatch.StartNew();
while (input != "Exit")
{
  Console.WriteLine();
  Console.WriteLine("===================Start Cases===================");
  Console.Write("Input length of test data :  ");
  input = Console.ReadLine();
  int limit = -1;
  Int32.TryParse(input, out limit);
  if(limit > 0)
  {
    using (Packet packet = new Packet(limit))
    {
      packet.Text = "Test Text";
      packet.Types = PacketType.Text;
      Console.WriteLine("Test start with {0} Datas", limit * 2);
      Console.WriteLine();
      sw = Stopwatch.StartNew();
      string t = JsonSerializer.Serialize(packet);
      Console.WriteLine("{1}ms\t| JSON : {0}", ByteFormat(Encoding.Default.GetBytes(t).Length), sw.Elapsed.TotalMilliseconds);
      File.WriteAllText("Data.txt", t);
      Console.WriteLine("==================End Testcase===================");
      Console.WriteLine();
      int id = GC.GetGeneration(t);
      t = String.Empty;
      GC.Collect(id, GCCollectionMode.Forced);
      //packet.Dispose();
      //GC.SuppressFinalize(packet);
      //GC.SuppressFinalize(t);
    }
  }
  GC.Collect();
}
