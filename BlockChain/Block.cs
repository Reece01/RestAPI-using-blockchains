using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebLoginApi.BlockChain
{
    public class Block
    {
        //public static Blockchain Chain { get; } = Program.Chain;

        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Packeages { get; set; }
        public int Pickle { get; set; }

        public Block(DateTime timeStamp, string Previoushash, string User, string Pass, string Pack, int Pick)
        {
            Index = 0;
            timeStamp = TimeStamp;
            PreviousHash = Previoushash;
            Hash = CalculateHash();

            Username = User;
            Pickle = Pick;
            Password = Pass;
            Packeages = Pack;
            if (Password != null)
            {
                var NewPass = HashEncrypt();
                if (NewPass != null) {
                    Password = NewPass;
                    Pickle = 1;
                }
            }
            else
            {
                Password = null;
            }
        }

        public string HashEncrypt()
        {
            if (Password == "") { return null; }
            if (Pickle == 1) { return null; }

            using (StreamWriter file = File.CreateText(@"Encryption.txt"))
            {
                file.Write(JsonConvert.SerializeObject(Encrypt.EncryptString(Password, Program.Chain.GetLatestBlock().Hash + Username) + " : " + Program.Chain.GetLatestBlock().Hash + Username + Password));
                file.Dispose();
            }

            return Encrypt.EncryptString(Password, Program.Chain.GetLatestBlock().Hash + Username + Password);
        }

        public string CalculateHash()
        {
            SHA256 CalHash = SHA256.Create();

            byte[] input = Encoding.ASCII.GetBytes(TimeStamp + PreviousHash + JsonConvert.SerializeObject(Username) + JsonConvert.SerializeObject(Packeages));
            byte[] Output = CalHash.ComputeHash(input);

            return Convert.ToBase64String(Output);
        }
    }
}
