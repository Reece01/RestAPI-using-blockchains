using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLoginApi.BlockChain;

namespace WebLoginApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Packages : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"/Packages"))
            {
                Console.WriteLine("Packages Folder Does not exist! Creating " + Environment.CurrentDirectory + @" / Packages");
                Directory.CreateDirectory(Environment.CurrentDirectory + @"/Packages");
            }
            return "Package Manager";
        }

        [HttpPost]
        public string Post([FromBody] string value) // Username:Reece|Password:Reece123|Packages:Loader,More,And,More
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"/Packages"))
            {
                Console.WriteLine("Packages Folder Does not exist! Creating " + Environment.CurrentDirectory + @" / Packages");
                Directory.CreateDirectory(Environment.CurrentDirectory + @"/Packages");
            }
            string[] FilePaths = Directory.GetFiles(Environment.CurrentDirectory + @"/Packages", "*.bin");

            string[] ValuePaser = value.Split('|');
            string Username = ValuePaser[0].Split(':')[1];
            string Password = ValuePaser[1].Split(':')[1];
            string Packages = ValuePaser[2].Split(':')[1];
            string[] Package = Packages.Split(',');

            foreach (Block I in Program.Chain.Chain)
            {
                if(Username == I.Username)
                {
                    if (Username == "Dummy")
                    {
                        foreach (var D in FilePaths)
                        {
                            if (D.Contains("Loader"))
                            {
                                var a = System.IO.File.ReadAllText(D);
                                return Encrypt.EncryptString(a, I.Username + Password);
                            }
                        }
                    }
                    else
                    {
                        if (Password == Encrypt.DecryptString(I.Password, I.PreviousHash + Username + Password))
                        {
                            foreach (var D in FilePaths)
                            {
                                if (D.Contains(Packages))
                                {
                                    var a = System.IO.File.ReadAllText(D);
                                    return Encrypt.EncryptString(a, I.PreviousHash + Username + Password);
                                }
                                else
                                {
                                    return Username + "|" + Password + "|" + Packages + " #6";
                                }
                            }
                        }
                    }
                }
            }
            return Username + "|" + Password + "|" + Packages + " #1";
        }
    }
}
