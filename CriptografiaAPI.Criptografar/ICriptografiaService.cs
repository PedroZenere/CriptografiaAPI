﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptografiaAPI.Criptografar
{
    public interface ICriptografiaService
    {
        string EncryptString(string plainText);
        string DecryptString(string cipherText);
    }
}
