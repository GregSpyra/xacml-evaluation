using Xacml.Core;
using Xacml.Core.Runtime;
using System;
using System.Xml;

namespace XacmlTest
{
  internal class MainClass
  {
    private static void Main(string[] args)
    {
      string policyDocument = string.Empty;
      string contextDocument = string.Empty;
      bool verbose = false;
      foreach (string str in args)
      {
        if ((int) str[0] == 47 || (int) str[0] == 45)
        {
          if ((int) str[1] == 112 || (int) str[1] == 80)
            policyDocument = str.Substring(3);
          if ((int) str[1] == 114 || (int) str[1] == 82)
            contextDocument = str.Substring(3);
          if ((int) str[1] == 118 || (int) str[1] == 86)
            verbose = true;
        }
      }
      try
      {
        if (contextDocument.Length == 0 || policyDocument.Length == 0)
          throw new Exception("Request or policy file not specified.");
        new EvaluationEngine(verbose).Evaluate(policyDocument, contextDocument, XacmlVersion.Version11).WriteDocument((XmlWriter) new XmlTextWriter(Console.Out)
        {
          Formatting = Formatting.Indented
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("\t-p:[policyFilePath]  - The path to the policy file");
        Console.WriteLine("\t-r:[requestFilePath] - The path to the request file");
        Console.WriteLine("\t-v                   - Makes the execution verbose");
      }
    }
  }
}
