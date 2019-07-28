using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebLoginApi.BlockChain
{
    public class ReadChainFile
    {
        private readonly Blockchain ToSave = Program.Chain;

        public ReadChainFile()
        {

            string a;
            if (File.Exists(@"LoginChain.txt"))
            {
                var fileStream = new FileStream(@"LoginChain.txt", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    a = streamReader.ReadToEnd();

                    var adress = JsonConvert.DeserializeObject<Blockchain>(a);

                    foreach (Block I in adress.Chain)
                    {
                        using (StreamWriter file = File.CreateText(@"Read.txt"))
                        {
                            if (I.Username != "Server")
                                if (!ToSave.Chain.Contains(I))
                                {
                                    file.WriteLine(I.TimeStamp + ":" + I.PreviousHash + ":" + I.Username + ":" + I.Password + ":" + I.Packeages + ":" + I.Pickle);
                                    ToSave.AddBlock(new Block(I.TimeStamp, I.PreviousHash, I.Username, I.Password, I.Packeages, I.Pickle));
                                }
                                else
                                {
                                    file.WriteLine("=============================");
                                    file.WriteLine(JsonConvert.SerializeObject(a));
                                    file.WriteLine(JsonConvert.SerializeObject(I));
                                    file.Dispose();
                                }
                        }
                    }
                }
            }

        }
    }
}
