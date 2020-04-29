namespace FsiRhinoTest

open Rhino
open FSharp.Compiler.Interactive.Shell
open System.IO


module Rh = 
    let print text = 
        RhinoApp.WriteLine text  
        RhinoApp.Wait()


type SeffTestPlugin () =  // the Plugin singelton

    inherit PlugIns.PlugIn()

    static member val Instance = SeffTestPlugin() // singelton pattern     

    override this.OnLoad refErrs = 
            //Rh.print  "loaded FsiRhinoTest Plugin"   

            //try to explicitly load FSharp.Core.dll
            let assem = this.Assembly
            if isNull assem then Rh.print "ERROR: cannot get pulgin assembly "
            else                 
                Rh.print ("plugin loaded from: " + assem.Location)
                let path = Path.Combine(Path.GetDirectoryName(assem.Location),"FSharp.Core.dll")
                let fcAss = System.Reflection.Assembly.LoadFile(path)  
                if isNull fcAss then 
                    Rh.print ("ERROR: cannot load "+path)
                else 
                    Rh.print ("loaded "+path)
            
            PlugIns.LoadReturnCode.Success
  


  type FsiRhinoTestCmd () = // the only Command  of this plugin
      inherit Commands.Command()    
      
      static member val Instance = FsiRhinoTestCmd() // singelton pattern
      
      /// the name to type into the rhino command line
      override this.EnglishName = "FsiRhinoTest" 
             
      override this.RunCommand (doc, mode)  =
          Rh.print  "FsiRhinoTestCmd start..."          
  
          let inn = new StringReader("")
          let out = new RhinoApp.CommandLineTextWriter()
          let config = FsiEvaluationSession.GetDefaultConfiguration()
          let noArgs = [| "the first arg is ignored "|]
          let session = FsiEvaluationSession.Create(config, noArgs, inn, out, out)
  
          Rh.print  "Hurray! FsiEvaluationSession.Created! wait for evaluation ..." 

          session.EvalInteraction("let theAnswer = 40 + 2")

          Rh.print  "is 42 the answer?" 
          Commands.Result.Success