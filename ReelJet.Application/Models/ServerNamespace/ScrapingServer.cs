using System;
using System.Linq;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;

namespace ReelJet.Application.Models.ServerNamespace.Abstracts;

public class ScrapingServer {
    
    public string ServerName { get; set; }
    public string VideoPageUrl { get; set; }
    public string VideoFrameUrl { get; set; }
    public ObservableCollection<Option> Options { get; set; }
} 
