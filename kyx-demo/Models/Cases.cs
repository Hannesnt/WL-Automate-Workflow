using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Cases
{
    [JsonPropertyName("objectDescribe")]
    public ObjectDescribe ObjectDescribe { get; set; }

    [JsonPropertyName("recentItems")]
    public List<RecentItem> RecentItems { get; set; }
}

public class ObjectDescribe
{
    public bool Activateable { get; set; }
    public object AssociateEntityType { get; set; }
    public object AssociateParentEntity { get; set; }
    public bool Createable { get; set; }
    public bool Custom { get; set; }
    public bool CustomSetting { get; set; }
    public bool DeepCloneable { get; set; }
    public bool Deletable { get; set; }
    public bool DeprecatedAndHidden { get; set; }
    public bool FeedEnabled { get; set; }
    public bool HasSubtypes { get; set; }
    public bool IsInterface { get; set; }
    public bool IsSubtype { get; set; }
    public string KeyPrefix { get; set; }
    public string Label { get; set; }
    public string LabelPlural { get; set; }
    public bool Layoutable { get; set; }
    public bool Mergeable { get; set; }
    public bool MruEnabled { get; set; }
    public string Name { get; set; }
    public bool Queryable { get; set; }
    public bool Replicateable { get; set; }
    public bool Retrieveable { get; set; }
    public bool Searchable { get; set; }
    public bool Triggerable { get; set; }
    public bool Undeletable { get; set; }
    public bool Updateable { get; set; }
    public Urls Urls { get; set; }
}

public class Urls
{
    public string CompactLayouts { get; set; }
    public string RowTemplate { get; set; }
    public string ApprovalLayouts { get; set; }
    public string CaseArticleSuggestions { get; set; }
    public string CaseRowArticleSuggestions { get; set; }
    public string Listviews { get; set; }
    public string Describe { get; set; }
    public string QuickActions { get; set; }
    public string Layouts { get; set; }
    public string Sobject { get; set; }
}

public class RecentItem
{
    [JsonPropertyName("attributes")]
    public Attributes Attributes { get; set; }

    [JsonPropertyName("Id")]
    public string Id { get; set; }

    [JsonPropertyName("CaseNumber")]
    public string CaseNumber { get; set; }
}

public class Attributes
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}
