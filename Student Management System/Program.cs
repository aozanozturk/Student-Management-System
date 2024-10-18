using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace Student_Management_System
{
    internal class Program
    {

        private static List<int> studentId = new List<int>();
        private static List<string> firstNames = new List<string>();
        private static List<string> lastNames = new List<string>();
        private static List<int> ages = new List<int>();
        private static List<double> averageGrades = new List<double>();
        private static List<string> studentInformation = new List<string>();//Tüm bilgileri içinde tutup txt dosyasının içine yazmayı denedim ama olmadı.
        static void Main(string[] args)
        {
           
            int id = 0;
            string name = string.Empty;
            bool cikis = true;
            while (cikis)
            {
                string choice = GetStringFromUser("Yapmak istediğiniz işlemi seçiniz.\n1: Tüm öğrencileri listele.\n2: Yeni öğrenci ekle.\n3: Öğrenci sil.\n4: Soyadına göre öğrenci ara.\n5: Çıkış yap.");
                switch (choice)
                {
                    case "1":
                        ListAllStudents();
                        break;
                    case "2":
                        AddStudentIds();
                        AddStudentNames();
                        AddStudentSurnames();
                        AddStudentAges(); 
                        AddStudentAverageGrades();
                        SaveStudentsToFile(studentInformation, GetStringFromUser("Bilgileri kaydetmek istediğiniz dosyanın yolunu giriniz."));
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        SearchStudentByLastName();
                        break;
                    case "5":
                        cikis = false;
                        break;
                        default:
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("Geçerli bir işlem seçiniz!!!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

            }
            static void SaveStudentsToFile(List<string> strings, string filePath)
            {
                string deneme = "";
                for (int i = 0; i < studentId.Count; i++)
                {
                    deneme += studentInformation[i]+"\n";
                }
                File.WriteAllText(filePath, deneme);
            }

            void ListAllStudents()
            {
                for (int i = 0; i < studentId.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.Öğrencinin numarası:{studentId[i]}, Adı:{firstNames[i]}, Soyadı:{lastNames[i]}, Yaşı:{ages[i]}, Not Ortalaması:{averageGrades[i]}.\n*******************");
                }
            }//Bilgileri kaydedilen öğrencileri listeler.

            void DeleteStudent()
            {
                bool delete = true;
                do
                {
                    id = NumberControlAndReturn();

                    if (id < 1000 || id > 9999)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Öğrenci numarası 4 haneli olmalıdır!!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (!studentId.Contains(id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Girdiğiniz numaraya sahip bir öğrenci bulunmamaktadır!!Tekrar deneyiniz.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (studentId.Contains(id))
                    {
                        studentId.Remove(id);
                        Console.WriteLine("Girdiğiniz numaraya sahip olan öğrenci silinmiştir.");
                        delete = false;
                    }

                }
                while (delete);
            }//Numarası girilen öğrenciyi siler.

            void SearchStudentByLastName()
            {
                bool search = true;
                int index = 0;
                string lastName = string.Empty;
                do
                {
                     lastName = GetStringFromUser("Öğrencinin soyadını giriniz.");
                    if (IsName(lastName))
                    {
                        if (lastNames.Contains(lastName))
                        {
                            index = lastNames.IndexOf(lastName);
                            Console.WriteLine($"{lastName} soyadına sahip öğrencinin Numarası:{studentId[index]}, Adı:{firstNames[index]}, Yaşı:{ages[index]}, Not Ortalaması:{averageGrades[index]}");
                            search = false;
                        }
                        else
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Girdiğiniz soyadına sahip bir öğrenci bulunmamaktadır.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lütfen düzgün bir soyadı giriniz!");
                    Console.ForegroundColor = ConsoleColor.White;

                    //lastName = GetStringFromUser("Aramak istediğiniz öğrencinin soyadını giriniz.");

                    //if (lastNames.Contains(lastName))
                    //{
                    //    index = lastNames.IndexOf(lastName);
                    //    Console.WriteLine($"{lastName} soyadına sahip öğrencinin Numarası:{studentId[index]}, Adı:{firstNames[index]}, Yaşı:{ages[index]}, Not Ortalaması:{averageGrades[index]}");
                    //    search = false;
                    //}
                }
                while (search);
            }//Soyadına göre öğrenci aratır.

            static bool IsName(string strName)
            {

                return !string.IsNullOrWhiteSpace(strName) && IsLetter(strName) && strName.Length >= 2 && 50 >= strName.Length;
            }//Girilenin isim olup olmadığını kontrol ediyor.

            static bool IsLetter(string strInput)
            {
                foreach (char character in strInput)
                {
                    if (!char.IsLetter(character))
                    {
                        return false;
                    }
                }
                return true;
            }//Girilen inputun harflerden oluşup oluşmadığını kontrol edip doğruysa döndürür.
            
            static void AddStudentNames()
            {

                bool isName = true;
                do
                {
                    string names = GetStringFromUser("Öğrencinin adını giriniz.");
                    if (IsName(names))
                    {
                        firstNames.Add(names);
                        studentInformation.Add(names);
                        break;

                    }
                    else
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lütfen düzgün bir isim giriniz!");
                    Console.ForegroundColor = ConsoleColor.White;
                    isName = false;
                }
                while (!isName);
            }//Kontroller yapıldıktan sonra adları listeye ekler.

            static void AddStudentSurnames()
            {


                bool isSurname = true;
                do
                {
                    string surnames = GetStringFromUser("Öğrencinin soyadını giriniz.");
                    if (IsName(surnames))
                    {
                        lastNames.Add(surnames);
                        studentInformation.Add(surnames);
                        break;

                    }
                    else
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lütfen düzgün bir soyadı giriniz!");
                    Console.ForegroundColor = ConsoleColor.White;
                    isSurname = false;
                }
                while (!isSurname);
            }//Kontroller yapıldıktan sonra soyadları listeye ekler.

            static void AddStudentAges()
            {
                int age = 0;
                bool addAge = true;


                do
                {
                    age = NumberControlAndReturnAge();



                    if (age > 0 && age < 91)
                    {
                        ages.Add(age);
                        studentInformation.Add(age.ToString());
                        addAge = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lütfen doğru bir yaş giriniz!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                while (addAge);

            }//Yaş kontrolü yaptıktan sonra yaşları listeye ekler.

            static void AddStudentIds()
            {
                int no = 0;
                bool addNum = true;


                do
                {
                    no = NumberControlAndReturn();
                    if (studentId.Contains(no))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Öğrencilerin numarası aynı olamaz!");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    if (no >= 1000 && no <= 9999)
                    {
                        studentId.Add(no);
                        studentInformation.Add(no.ToString());
                        addNum = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lütfen 4 haneli numara giriniz!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                while (addNum);

            } //Girilen numaranın 4 haneli ve tekrar etmeyen olduğunu kontrol edip studentId listesinin içine ekler.

            static void AddStudentAverageGrades()
            {
                double averageGrade = 0;
                bool addAverage = true;


                do
                {
                    averageGrade = NumberControlAndReturnDouble();



                    if (averageGrade >= 0 && averageGrade <= 100)
                    {
                        averageGrades.Add(averageGrade);
                        studentInformation.Add(averageGrade.ToString());
                        addAverage = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lütfen doğru bir not giriniz!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                while (addAverage);

            }//Not ortalaması girilen öğrencilerin not ortalamalarını listeye ekler.

            static double NumberControlAndReturnDouble()
            {
                bool isItNumber = true;
                double dblNum = 0;
                do
                {

                    string strNum = GetStringFromUser("Öğrencinin not ortalamasını giriniz.");
                    isItNumber = IsItDouble(strNum, out dblNum);
                    if (!isItNumber)
                    {
                        Console.WriteLine("Lütfen sadece sayı giriniz!");

                    }




                }

                while (!isItNumber);
                return dblNum;

            }//Double kontrolü

            static int NumberControlAndReturnAge()
            {
                bool isItNumber = true;
                int intNum = 0;
                do
                {

                    string strNum = GetStringFromUser("Öğrencinin yaşını giriniz.");
                    isItNumber = IsItInteger(strNum, out intNum);
                    if (!isItNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lütfen sadece sayı giriniz!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }




                }

                while (!isItNumber);
                return intNum;

            }//Yaş için sayı kontrolü

            static int NumberControlAndReturn()
            {
                bool isItNumber = true;
                int intNum = 0;
                do
                {

                    string strNum = GetStringFromUser("Öğrencinin numarasını giriniz");
                    isItNumber = IsItInteger(strNum, out intNum);
                    if (!isItNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lütfen sadece sayı giriniz!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }




                }

                while (!isItNumber);
                return intNum;

            }//Girilen sayının sayı olup olmadığını kontrol eder ve sayıyı döndürür.

            static bool IsItInteger(string strnumber, out int intnumber)
            {
                return int.TryParse(strnumber, out intnumber);
            }//Sayının integer olup olmadığını bulur ve sayıyı döner(out).

            static string GetStringFromUser(string message)
            {
                Console.WriteLine(message);
                return Console.ReadLine();
            }//Kullanıcıdan string alır.

            static bool IsItDouble(string strnumber, out double dblNumber)
            {
                return double.TryParse(strnumber, out dblNumber);
            }//Double mı değil mi kontrol eder ve sayıyı döner.



        }
    }
}
