using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using tut2.Models;

namespace tut2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] arguments = { "Data/data.csv", "result.xml" , "xml" };

           // Example of wrong path for result file
           // arguments[1] = "result/.xml";


            string pathToCsvGiven = @arguments[0];
            string pathDestinationGiven = @arguments[1];
            var dataFormatGiven = arguments[2];

            string pathToLogFile = @"log.txt";
            using (StreamWriter logWriter = new StreamWriter("log.txt", false))
            {
                logWriter.WriteLine();
            }

            checkIfPathCorrect(pathDestinationGiven);

            var listOfStudents = new HashSet<Student>(new CustomComparer());
            var listOfStudiesName = new HashSet<studies>(new CustomComparerStudiesName()); //list of studies with diffrent names 
                                                                                            //(the same are not added due to use of comparator)

            try
            {
                FileInfo fileInfo = new FileInfo(pathToCsvGiven);
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

                            var studiesName = new studies
                            {
                                name = columns[2]
                            };

                            if (!listOfStudents.Add(student))
                            {
                                string content = "Found duplicate of or null argument " + line;
                                File.AppendAllText(pathToLogFile, content + Environment.NewLine);
                            }
                            else
                            {
                                listOfStudents.Add(student);
                                listOfStudiesName.Add(studiesName);
                            }
                        }
                        else
                        {
                            string content = "Missing arguments in " + line;
                            File.AppendAllText(pathToLogFile, content + Environment.NewLine);
                        }
                    }
                }

                using (FileStream fileStream = File.Open("result.xml", FileMode.Create))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(HashSet<Student>), new XmlRootAttribute("university"));
                    xmlSerializer.Serialize(fileStream, listOfStudents);
                }

                //Here I had problem how should I "attach" content of this function to the result.xml file as a part of 'university' attribute?
                getAllStudiesNamesWithStudentsNumbers(listOfStudents, listOfStudiesName);

            }
            catch(FileNotFoundException e)
            {
                using (StreamWriter logWriter = new StreamWriter("log.txt", false))
                {
                    logWriter.WriteLine(e.Message + Environment.NewLine + "File does not exis" + Environment.NewLine);
                }
                Console.WriteLine(e);
            }
          
        }


        public static void checkIfPathCorrect(string pathDestinationGiven)
        {
            Regex regex = new Regex(@"((?:[a-zA-Z]\\:){0,1}(?:[\\/][\\w.]+){1,})");
            if (regex.IsMatch(pathDestinationGiven))
            {
                using (StreamWriter logWriter = new StreamWriter("log.txt", false))
                {
                    logWriter.WriteLine("The path is incorrect" + Environment.NewLine);
                }
                System.Environment.Exit(1);
            }
        }

        public static void getAllStudiesNamesWithStudentsNumbers(HashSet<Student> listOfStudents, HashSet<studies> listOfStudiesName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement activeStudies = doc.CreateElement("activeStudies");
            foreach (studies element in listOfStudiesName)
            {
                int studiesCount = 0;
                foreach (Student student in listOfStudents)
                {
                    if (student.StudiesName.Equals(element.name)) studiesCount++;
                }
                XmlElement studies = doc.CreateElement("studies");
                studies.SetAttribute("name", element.name);
                studies.SetAttribute("numberOfStudents", studiesCount.ToString());
                activeStudies.AppendChild(studies);
                doc.AppendChild(activeStudies);
            }
            doc.Save("numbersOfStudies.xml");

            using (Stream input = File.OpenRead("numbersOfStudies.xml"))
            using (Stream output = new FileStream("result.xml", FileMode.Append,
                                                  FileAccess.Write, FileShare.None))
            {
                input.CopyTo(output);
            }
            File.Delete("numbersOfStudies.xml");
        }

    }
}
