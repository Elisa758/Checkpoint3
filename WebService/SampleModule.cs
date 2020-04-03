using System;
using System.Collections.Generic;
using System.Text;
using Nancy;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Nancy.Responses;
using Newtonsoft.Json;
using System.Net;

namespace WebService
{
    public class SampleModule : NancyModule
    {
        public SampleModule()
        {
            Get(@"/file/{Filepath*}", parameters => DisplayFileContent(parameters.Filepath));

            Delete(@"/file/{Filepath*}", parameters => DeleteFile(parameters.Filepath));
            Put(@"/file/{Filepath*}", parameters => CreateNewFile(parameters.Filepath));

        }

        public dynamic DisplayFileContent(string filepath)
        {
           if (System.IO.File.Exists(filepath))
           {
                string[] text = System.IO.File.ReadAllLines(filepath);

                string jsonString;
                jsonString = System.Text.Json.JsonSerializer.Serialize(text);

                return jsonString;
            }
            else
            {
                string message = "This file doesn't exist";
                return message;
            }
        }

        public dynamic DeleteFile(string filepath)
        {
            string message;
            if (System.IO.File.Exists(filepath))
            {    
                System.IO.File.Delete(filepath);
                message = "File deleted.";
                string json = System.Text.Json.JsonSerializer.Serialize(message);
                return json;
            }
            else
            {
                message = "File not found";
                string json = System.Text.Json.JsonSerializer.Serialize(message);
                return json;
            }
        }
    

        public dynamic CreateNewFile(string filepath)
        {
            string message;
            if (!System.IO.File.Exists(filepath))
            {
                File file = new File();
                file.Name = filepath.Substring(filepath.LastIndexOf('/'));
                file.Filepath = filepath;

                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Text exemple in the file");
                    fs.Write(info, 0, info.Length);
                }
                message = "file created";
                string json = System.Text.Json.JsonSerializer.Serialize(message);
                return json;
            }
            else
            {
                message = "This files already exists";
                string json = System.Text.Json.JsonSerializer.Serialize(message);
                return json;
            }
        }
    }
}
