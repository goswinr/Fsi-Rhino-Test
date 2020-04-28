This repro is a minimal [Rhino3d](https://www.rhino3d.com/) plugin for hosting Fsharp.Compiler.Service.
Everything works fine when the PackageReference  of FSharp.Compiler.Service is set to  Version **33.0.1**
However just setting the FSharp.Compiler.Service  Version to **34.0.1** and calling 
`FsiEvaluationSession.Create`
it prints 
    
    Microsoft (R) F# Interactive version  for F# 
    Copyright (c) Microsoft Corporation. All Rights Reserved.

    For help type #help;;

    > 

    error FS0084: Assembly reference 'FSharp.Core.dll' was not found or is invalid

### How to reproduce the bug:
- download a [evaluation version of Rhino](https://www.rhino3d.com/download/rhino-for-windows/6/evaluation) if you don't have it yet
- build this repro
- start Rhino and drag and drop the `.rhp` file from the bin folder into Rhino to load the plugin.
- type `FsiRhinoTest` into the Rhino command line to creat an FSI evaluation session. This will print the above message to the Rhino command line window.

### How to have it work fine:
- close Rhino
- change the FCS version to `33.0.1` in the fsproj file
- rebuild
- start Rhino and type `FsiRhinoTest` to see the success message. (no need to drag and drop the `.rhp` file again)
