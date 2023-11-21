using System.Xml.Serialization;

namespace ParserAPI.Domain;

[XmlRoot(ElementName = "transfer")]
public class ParsedTransferDDO
{

    [XmlElement(ElementName = "reference")]
    public string Reference { get; set; }

    [XmlElement(ElementName = "from")]
    public string From { get; set; }

    [XmlElement(ElementName = "to")]
    public string To { get; set; }

    [XmlElement(ElementName = "amount")]
    public int Amount { get; set; }

    [XmlElement(ElementName = "currency")]
    public string Currency { get; set; }
}