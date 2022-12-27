using System;
using SharpTinyArgParser;

namespace Demo
{
    internal class Program
    {
        class ScanContext
        {
            [ArgsParser.Args("host", "127.0.0.1", Required = true)]
            public string[] Hosts { get; set; }
            [ArgsParser.Args("port", "135,440-445", Required = true)]
            public int[] Ports { get; set; }
        }
        static void Main(string[] args)
        {
            // get help
            string helpMsg = ArgsParser.PrintHelp(typeof(ScanContext), @"                                                                                   
 PPPPPPP                                    PPPPP                                  
 PPPPPPPP                                  PPPPPPP                                 
 PPP   PPP                        PPP     PPP  PPPP                                
 PPP   PPP                        PPP     PPP   PPP                                
 PPP   PPP                        PPP     PPP                                      
 PPP   PPP  PPPPPPP   PPPPPPPP PPPPPPPPP  PPPPP       PPPPPP    PPPPPP   PPPPPPPP  
 PPP  PPPP PPPP PPPP  PPPPPP      PPP      PPPPPP    PPP PPPP  PPP  PPP  PPPP PPPP 
 PPPPPPPP  PPP   PPP  PPPP        PPP         PPPP  PPP   PPP  P    PPP  PPP   PPP 
 PPP       PPP   PPPP PPPP        PPP          PPPP PPP           PPPPP  PPP   PPP 
 PPP       PPP   PPPP PPP         PPP    PPPP   PPP PPP        PPPPPPPP  PPP   PPP 
 PPP       PPP   PPP  PPP         PPP    PPPP   PPP PPP   PPP PPPP  PPP  PPP   PPP 
 PPP       PPPP  PPP  PPP         PPP     PPP   PPP PPPP  PPP PPPP  PPP  PPP   PPP 
 PPP        PPPPPPP   PPP         PPPPPPP PPPPPPPP   PPPPPPPP  PPPPPPPP  PPP   PPP 
 PPP         PPPPP    PPP          PPPPP    PPPPP      PPPP     PPPPPPPP PPP   PPP 
", "portscan");

            // check input args
            if (args.Length == 0)
            {
                Console.WriteLine(helpMsg);
                return;
            }

            ScanContext scanContext;

            //Catching exceptions for parameter conversion
            try
            {
                scanContext = ArgsParser.ParseArgs<ScanContext>(args);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    e = e.InnerException;
                }

                Console.WriteLine(e.Message);
                return;
            }

            foreach (string host in scanContext.Hosts)
            {
                foreach (int port in scanContext.Ports)
                {
                    Console.WriteLine($"scan host:{host} port:{port}");
                }
            }

        }
    }
}