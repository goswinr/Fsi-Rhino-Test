namespace FsiRhinoTest

open Rhino
open System
open System.Windows
open Rhino.Runtime
open FSharp.Compiler.Interactive.Shell
open System.IO
open System.Diagnostics


module Rh = 
    let print a    = RhinoApp.WriteLine a    ; RhinoApp.Wait()


type SeffTestPlugin () =  

    inherit PlugIns.PlugIn()

    static member val Instance = SeffTestPlugin() // singelton pattern     

    override this.OnLoad refErrs = 
            Rh.print  "loaded FsiRhinoTest Plugin"   
            PlugIns.LoadReturnCode.Success
  
  

  type FsiRhinoTestInitCmd () = // a dummy command just to make the plugin load
      inherit Commands.Command()    
     
      static member val Instance = FsiRhinoTestInitCmd() 
      
      override this.EnglishName = "FsiRhinoTestInit" 
             
      override this.RunCommand (doc, mode)  =
          Rh.print  "FsiRhinoTestInit ran."
          Commands.Result.Success
  

  type FsiRhinoTestCreateSessionCmd () = 
      inherit Commands.Command()    
      
      static member val Instance = FsiRhinoTestCreateSessionCmd() 
      
      override this.EnglishName = "FsiRhinoTestCreateSession" 
             
      override this.RunCommand (doc, mode)  =
          Rh.print  "FsiRhinoTestCreateSessionCmd start..."
          
  
          let inn = new StringReader("")
          let out = new RhinoApp.CommandLineTextWriter()
          let config = FsiEvaluationSession.GetDefaultConfiguration()
          let noArgs = [| "the first arg is ignored "|]
          let session = FsiEvaluationSession.Create(config, noArgs, inn, out, out)

  
          Rh.print  "Hurray! FsiEvaluationSession.Created!" 

          session.EvalInteraction("let theAnswer = 40 + 2")

          Rh.print  "is 42 the answer?" 
          Commands.Result.Success