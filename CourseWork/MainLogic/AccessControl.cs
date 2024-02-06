using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using CourseWork.DataConverter;
using System.Security.Cryptography;
using System.Text;
using System;
using CourseWork.DataBase;
using CourseWork.ViewModel;

namespace CourseWork.MainLogic;

public class AccessControl:IAccessControl
{
    
    private IHaveAuthorizeInfo _source; 
    public AccessControl(IHaveAuthorizeInfo source)
    {
        _source = source;
    }
   

    public List<IWorkProfile> Authorize(string login, string password)
    {
        if (login == null)
        {
            throw new ArgumentException("Login will not be empty");
        }
        if (password == null)
        {
            throw new ArgumentException("Password will not be empty");
        }
        if (_source.AuthorizeList != null)
        {
            foreach (var item in _source.AuthorizeList)
            {
                if (item.Login == login)
                {
                    string saltedPassword = GenerateSaltPassword(password, item.Salt);
                    if (saltedPassword == item.Password) return _source.FindWorkProfile(item);
                    throw new ArgumentException("Wrong login or password");
                }
            }

            throw new ArgumentException("Wrong login or password");
        }

        throw new ArgumentException("Authorize list was not found");
    }
    public static byte[] GenerateSalt()
    {
        int saltLength = 64;
        byte[] salt = new byte[saltLength];
        var rngRand = new RNGCryptoServiceProvider();
        rngRand.GetBytes(salt);
        return salt;
    }
    public static byte[] GenerateSha256Hash(string password, byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];
        Buffer.BlockCopy(passwordBytes,0,saltedPassword,0,passwordBytes.Length);
        Buffer.BlockCopy(salt,0,saltedPassword,passwordBytes.Length,salt.Length);
        return SHA256.HashData(saltedPassword);
    }
    public static string GenerateSaltPassword(string password, byte[] salt)
    {
        return BitConverter.ToString(GenerateSha256Hash(password, salt));
    }
}