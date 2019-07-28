using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebLoginApi.BlockChain
{
    public class SaveBlockChain
    {
        private readonly Blockchain ToSave = Program.Chain;

        public SaveBlockChain()
        {
            try
            {
                using (StreamWriter file = File.CreateText(@"LoginChain.txt"))
                {
                    file.Write(JsonConvert.SerializeObject(ToSave));
                    file.Dispose();
                }
            }
            catch(Exception ex)
            {
                using (StreamWriter file = File.CreateText(@"ERRORWriting.txt"))
                {
                    file.Write(JsonConvert.SerializeObject(ex.Source));
                    file.Dispose();
                }

                if (ex.Source == "")
                {
                    File.Delete(@"LoginChain.txt");
                }
            }
        }
    }
}
