using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CrtajMeModel
{
    public class Tester
    {
        private static UlazniObrazac _ulazniObrazac;
        private static IzlazniObrazac _izlazniObrazac;
        private static Prikaz _prikaz;
        private static Process _aplikacija;

        public static void Config(string appName, Prikaz view, UlazniObrazac input, IzlazniObrazac output)
        {
            _aplikacija = new Process();
            _aplikacija.StartInfo.FileName = appName;
            _ulazniObrazac = input;
            _izlazniObrazac = output;
            _prikaz = view;
            ResetTester();
        }


        /// <summary>
        /// Saljde se ulaz vanjskoj aplikaciji
        /// </summary>
        private static void InputStep()
        {
            string inputData = _ulazniObrazac.generateInput();
            _aplikacija.StandardInput.Write(inputData);
            _aplikacija.StandardInput.WriteLine("");
            _aplikacija.StandardInput.Flush();
        }


        /// <summary>
        /// Cita se izlaz iz vanjske aplikacije
        /// </summary>
        private static void OutputStep()
        {
            //cita se izlaz od aplikacije i procesira se dalje
            string output;
            
            while (true)
            {
                if (_aplikacija.HasExited == true) 
                    break; 
                output = _aplikacija.StandardOutput.ReadLine();
                Console.WriteLine("Izlaz:"+output);
                List<double> parseOutput = _izlazniObrazac.sendOutput(output);
                foreach (double num in parseOutput)
                {
                    _prikaz.SaveNumber(num);
                }
            }
        }

        /// <summary>
        /// Metoda pocinje s testiranjem
        /// </summary>
        public static void StartTesting()
        {
            ResetTester();
            try
            {
                //pokretanje aplikacije
                _aplikacija.Start();
                
                InputStep();
                OutputStep();
            }
            catch(Exception e)
            {
                throw new CrtajMeException("Dogodila se greska prilikom testiranja");
           }
         }

        /// <summary>
        /// Zaustavlja se tester
        /// </summary>
        public static void StopApplication()
        {
            try
            {
                if (_aplikacija != null)
                    _aplikacija.Kill();
            }
            catch (InvalidOperationException e)
            {
                //ako proces jos nije povezan ovo je ok radnja
            }
        }

        /// <summary>
        /// Tester se resetira, tj. sva trenutna stanja postavi u pocetna
        /// </summary>
        public static void ResetTester()
        {
            _aplikacija.StartInfo.UseShellExecute = false;
            _aplikacija.StartInfo.RedirectStandardOutput = true;
            _aplikacija.StartInfo.RedirectStandardError = true;
            _aplikacija.StartInfo.RedirectStandardInput = true;
            _aplikacija.StartInfo.CreateNoWindow = true;
            _aplikacija.StartInfo.WorkingDirectory = "";
            StopApplication();
        }

    }
}
