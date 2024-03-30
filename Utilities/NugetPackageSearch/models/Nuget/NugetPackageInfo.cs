using System;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace NugetPackageSearch.models.Nuget;

public class NugetPackageInfo : nac.ViewModelBase.ViewModelBase
{

    public string Id
    {
        get { return GetValue(() => Id); }
        set { SetValue(() => Id, value);}
    }

    public string Version
    {
        get { return GetValue(() => Version); }
        set { SetValue(() => Version, value);}
    }

    
    public string ProjectUrl { get; set; }

    public long? DownloadCount
    {
        get { return GetValue(() => DownloadCount); }
        set { SetValue(() => DownloadCount, value);}
    }

    public string Description
    {
        get { return GetValue(() => Description); }
        set { SetValue(() => Description, value);}
    }

    public NugetPackageInfo(IPackageSearchMetadata nugetSearchMeta)
    {
        this.Id = nugetSearchMeta.Identity?.Id ?? "";
        this.Version = nugetSearchMeta.Identity?.Version?.ToString() ?? "";

        this.Description = nugetSearchMeta.Description;
        this.DownloadCount = nugetSearchMeta.DownloadCount;
        this.ProjectUrl = nugetSearchMeta.ProjectUrl?.AbsoluteUri ?? "";
    }



}