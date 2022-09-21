using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using PCLExt.FileStorage;
using Xamarin.Forms.Shapes;
using System.IO;
using System.Collections;
using static System.Net.WebRequestMethods;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using System.Runtime.InteropServices;


namespace VPassSample
{
    public static class CContexto
    {
        public static SQLiteConnection conexao;
        static IFolder pasta;
        static CContexto()
        {
            pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("InfoDB", PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);
            conexao = new SQLiteConnection(arquivo.Path);

            conexao.CreateTable<Models.CUsuario>();
            conexao.CreateTable<Models.CCategoria>();
            conexao.CreateTable<Models.CSenha>();
        }

        public static void Inserir<T>(T modelo)
        {
            conexao.Insert(modelo);
        }

        public static void Atualizar<T>(T modelo)
        {
            conexao.Update(modelo);
        }

        public static void Deletar<T>(T modelo)
        {
            conexao.Delete(modelo);
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }          
    }
}
