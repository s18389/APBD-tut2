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

            string pathToLogFile = @"log.txt";

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

            var listOfStudents = new HashSet<Student>(new CustomComparer());


            using (var streamReader = new StreamReader(fileInfo.OpenRead()))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    if (columns.Length == 9)
                    {
                        var student = new Student
                        {
                            fname = columns[0],
                            lname = columns[1],
                            StudiesName = columns[2],
                            StudiesMode = columns[3],
                            IndexNumber = columns[4],
                            birthdate = DateTime.Parse(columns[5]),
                            email = columns[6],
                            mothersName = columns[7],
                            fathersName = columns[8]
                        };

                        if (!listOfStudents.Add(student))
                        {
                            string content = "Found duplicate of or null argument " + line;
                            File.AppendAllText(pathToLogFile, content + Environment.NewLine);
                        }
                        else
                        {
                            listOfStudents.Add(student);
                        }
                    }
                    else
                    {
                        string content = "Missing arguments in " + line;
                        File.AppendAllText(pathToLogFile, content + Environment.NewLine);
                    }
                }
            }

            FileStream writer = new FileStream(@"result.xml", FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HashSet<Student>), new XmlRootAttribute("university"));

            xmlSerializer.Serialize(writer, listOfStudents);
        }
    }
}
