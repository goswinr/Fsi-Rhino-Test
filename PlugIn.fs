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


type SeffTestPlugin () =  // the Plugin singelton

    inherit PlugIns.PlugIn()

    static member val Instance = SeffTestPlugin() // singelton pattern     

    override this.OnLoad refErrs = 
            Rh.print  "loaded FsiRhinoTest Plugin"   
            PlugIns.LoadReturnCode.Success
  


  type FsiRhinoTestCmd () = // the only Command  of this plugin
      inherit Commands.Command()    
      
      static member val Instance = FsiRhinoTestCmd() // singelton pattern
      
      override this.EnglishName = "FsiRhinoTest" // type his into the rhino command line
             
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