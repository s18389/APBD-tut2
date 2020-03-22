using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using tut2.Models;

namespace tut2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] arguments = { "Data/data.csv", "result.xml" , "xml" };
            arguments[0] = "Data/data.csv";
            string pathToCsvGiven = @arguments[0];

            arguments[1] = "result.xml";
            string pathDestinationGiven = @arguments[1];

            arguments[2] = "xml";
            var dataFormatGiven = arguments[2];


            if (!File.Exists(pathToCsvGiven))
            {
                Console.WriteLine("File does not exist.");
                System.Environment.Exit(1);
            }

            Regex regex = new Regex(@"((?:[a-zA-Z]\\:){0,1}(?:[\\/][\\w.]+){1,})");
            if (regex.IsMatch(pathDestinationGiven))
            {
                Console.WriteLine("Sth wrong with destitanion to result");
                System.Environment.Exit(1);
            }

            //Reading from file
            var fileInfo = new FileInfo(pathToCsvGiven);
            

            using (var streamReader = new StreamReader(fileInfo.OpenRead()))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    Console.WriteLine(line);
                }
            }

    
            var listOfStudents = new HashSet<Student>(new CustomComparer());
            var student = new Student
            {
                Email = "someEmail@Email.com",
                IndexNumber = "s1234",
                FirstName = "Jackob",
                LastName = "Michalski"
            };

            var student2 = new Student
            {
                Email = "someEmail@Email.com",
                IndexNumber = "s1234",
                FirstName = "Jackob",
                LastName = "Michalski"
            };

            listOfStudents.Add(student);
            if (!listOfStudents.Add(student2))
            {
                Console.WriteLine("I found duplicate!" + student2);
            }
            listOfStudents.Add(student2);

            FileStream writer = new FileStream(@"result.xml", FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HashSet<Student>), new XmlRootAttribute("university"));

            xmlSerializer.Serialize(writer, listOfStudents);
        }
    }
}
