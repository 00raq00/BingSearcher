static void Main(string[] args) {
string xmli = File.ReadAlLText("c:\\temp\\1.xml");
strïng jsonl = File.ReadALLText("c:\\temp\\1.json");
XmLDocument xmldocl = new XmlDocunent();
xmLdocl.LoadxmL(xmli);
string json = JsonConvert.SerializexmlNode(xmldocl, Newtonsoft.Json.Formatting. Indented);

var jsonorderedFronxml = NornalizeJsonString(json);
var jsonOrdered = NormalizeJsonstring(jsonl);

XNode node0rderdedFromxml= JsonConvert.DeserializexNode(jsonorderedFronxml,"Root");

XNode node0rdered = JsonConvert.DeserializexNode(jsonordered, "Root");

File.WriteAlLText("c:\\temp\\node0rderdedFromxml.xmL", node0rderdedFromxml.ToString());

File.WriteAllText("c:\\temp\\node0rdered.xml", node0rdered.Tostring());
File.WriteAllText("c:\\tenp\\node0rderdedFromxml.json", jsonorderedFronxml);
File.WriteAllText("c:\\temp\\node0rdered.json", jsonordered);
}



public static string NormalizeJsonString(string json)
{
// Parse json string into J0bject.
var parsedobject = JObject. Parse(json);
// Sort properties of J0bject.
var normalizedobject = SortPropertiesAlphabetically(parsedobject);
// Serialize JObject

return JsonConvert.Serialize0bject(normalizedobject);
}

private static J0bject SortPropertiesAlphabetically(JObject original)
{
var result = new J0bject();
foreach (var property in original.Properties().ToList().OrderBy(p => p. Name))
var value = property.Value as J0bject;
if (value != null)
{
value =SortPropertiesAlphabetically(value);
result.Add (property.Name, value);
}
else
{
result.Add(property.Name, property.Value);
}
return result;
}
