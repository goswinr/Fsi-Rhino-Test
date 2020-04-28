namespace FsiRhinoTest.AssemblyInfo


open System.Runtime.InteropServices

// Without this Guid Rhino does not remeber the plugin after restart, setting <ProjectGuid> in the new SDK fsproj file does not to work.
[<assembly: Guid("fbae86d1-3fe3-4d07-8cd7-3ac37147a3c8")>] 


do ()