# TinyArgParser

TinyArgParser is a command processing program, it has less than 300 lines of code, it supports command line parameter processing and help generation


# Example
## Demo-PortScan

A simple example of port scanning, we declare the Hosts property of the ScanContext class as string[] so that the user can enter multiple Hosts


```
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
```

If you run this program without any arguments, help is displayed

```

C:\Test>portscan

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


Arguments:

        -host Required:True  (default 127.0.0.1)
        -port Required:True  (default 135,440-445)

Example:

portscan -host 127.0.0.1 -port 135,440-445


C:\Test>

```

Scan only one port and one host

```
C:\Test>portscan  -host 127.0.0.1 -port 135
scan host:127.0.0.1 port:135

C:\Test>

```

Only scan multiple ports and one host

SharpTinyArgParser also supports inputting arrays in a range such as (440-445)
```
C:\Test>portscan -host 127.0.0.1 -port 135,440-445
scan host:127.0.0.1 port:135
scan host:127.0.0.1 port:440
scan host:127.0.0.1 port:441
scan host:127.0.0.1 port:442
scan host:127.0.0.1 port:443
scan host:127.0.0.1 port:444
scan host:127.0.0.1 port:445

C:\Test>
```

Scan multiple ports and multiple hosts

```

C:\Test>portscan -host 127.0.0.1,8.8.8.8 -port 135,440-445
scan host:127.0.0.1 port:135
scan host:127.0.0.1 port:440
scan host:127.0.0.1 port:441
scan host:127.0.0.1 port:442
scan host:127.0.0.1 port:443
scan host:127.0.0.1 port:444
scan host:127.0.0.1 port:445
scan host:8.8.8.8 port:135
scan host:8.8.8.8 port:440
scan host:8.8.8.8 port:441
scan host:8.8.8.8 port:442
scan host:8.8.8.8 port:443
scan host:8.8.8.8 port:444
scan host:8.8.8.8 port:445

C:\Test>
```

## Demo-Potato

We set PotatoContext.CommandLine to Required

```
using System;
using SharpTinyArgParser;

namespace Demo
{
    internal class Program
    {
        class PotatoContext
        {
            [ArgsParser.Args("clsid", "4991D34B-80A1-4291-83B6-3328366B9097")]
            public string Clsid { get; set; }
            [ArgsParser.Args("cmd", "cmd /c whoami", Required = true)]
            public string CommandLine { get; set; }
        }
        static void Main(string[] args)
        {
            // get help
            string helpMsg = ArgsParser.PrintHelp(typeof(PotatoContext), @"                                                                                   
                                                               
 PPPPPPP                                                       
 PPPPPPPP                                                      
 PPP   PPP             PPPP                 PPP                
 PPP   PPP             PPPP                 PPP                
 PPP   PPP             PPPP                 PPP                
 PPP   PPP  PPPPPPP  PPPPPPPPP   PPPPPPP PPPPPPPPP    PPPPPP   
 PPP  PPPP PPPP PPPP   PPPP     PPPP PPP    PPP      PPP PPPP  
 PPPPPPPP  PPP   PPP   PPPP     PP   PPP    PPP     PPP   PPP  
 PPP       PPP   PPPP  PPPP        PPPPP    PPP     PPP   PPPP 
 PPP       PPP   PPPP  PPPP     PPPPPPPP    PPP     PPP   PPPP 
 PPP       PPP   PPP   PPPP     PPP  PPP    PPP     PPP   PPPP 
 PPP       PPPP  PPP    PPP    PPPP  PPP    PPP     PPPP  PPP  
 PPP        PPPPPPP     PPPPPP  PPPPPPPP    PPPPPPP  PPPPPPP   
 PPP         PPPPP       PPPPP   PPPP PPP     PPPP     PPPP    
", "potato");

            // check input args
            if (args.Length == 0)
            {
                Console.WriteLine(helpMsg);
                return;
            }

            PotatoContext potatoContext;

            //Catching exceptions for parameter conversion
            try
            {
                potatoContext = ArgsParser.ParseArgs<PotatoContext>(args);
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

            Console.WriteLine($"[*] Clsid:{potatoContext.Clsid}");
            Console.WriteLine($"[*] CommandLine:{potatoContext.CommandLine}");

        }
    }
}
```

If you run this program without any arguments, help is displayed

```

C:\Test>potato


 PPPPPPP
 PPPPPPPP
 PPP   PPP             PPPP                 PPP
 PPP   PPP             PPPP                 PPP
 PPP   PPP             PPPP                 PPP
 PPP   PPP  PPPPPPP  PPPPPPPPP   PPPPPPP PPPPPPPPP    PPPPPP
 PPP  PPPP PPPP PPPP   PPPP     PPPP PPP    PPP      PPP PPPP
 PPPPPPPP  PPP   PPP   PPPP     PP   PPP    PPP     PPP   PPP
 PPP       PPP   PPPP  PPPP        PPPPP    PPP     PPP   PPPP
 PPP       PPP   PPPP  PPPP     PPPPPPPP    PPP     PPP   PPPP
 PPP       PPP   PPP   PPPP     PPP  PPP    PPP     PPP   PPPP
 PPP       PPPP  PPP    PPP    PPPP  PPP    PPP     PPPP  PPP
 PPP        PPPPPPP     PPPPPP  PPPPPPPP    PPPPPPP  PPPPPPP
 PPP         PPPPP       PPPPP   PPPP PPP     PPPP     PPPP


Arguments:

        -clsid Required:False  (default 4991D34B-80A1-4291-83B6-3328366B9097)
        -cmd Required:True  (default cmd /c whoami)

Example:

potato -cmd "cmd /c whoami"
potato -clsid 4991D34B-80A1-4291-83B6-3328366B9097 -cmd "cmd /c whoami"


C:\Test>
```


If you run this program without cmd parameters, the missing parameters will be displayed

```
C:\Test>potato -clsid 4991D34B-80A1-4291-83B6-3328366B9097
Required Parameter cmd

C:\Test>
```

Run with cmd parameters

```
C:\Test>potato -cmd whoami
[*] Clsid:4991D34B-80A1-4291-83B6-3328366B9097
[*] CommandLine:whoami

C:\Test>
```

Run with clsid and cmd parameters

```

C:\Test>potato -clsid abc -cmd whoami
[*] Clsid:abc
[*] CommandLine:whoami

C:\Test>
```

See SharpTinyArgParserUnitTest for more examples