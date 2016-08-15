namespace Sitecore.MediaFramework.Ooyala
{
  using Sitecore.Data;

  public static class FieldIDs
  {
    public static class Account
    {
      public static readonly ID ApiKey = new ID("{9F75219A-7090-4069-ABD7-38E61F163943}");

      public static readonly ID ApiSecret = new ID("{34606D48-0E23-4457-9E45-A468B7E32094}");
    }

    public static class MediaElement
    {
      public static readonly ID EmbedCode = new ID("{44562F9A-E283-42CD-963C-D1775C20FC41}");

      public static readonly ID Name = new ID("{F31B0620-300A-4A9C-B0A4-A90996811442}");

      public static readonly ID Description = new ID("{5D827F80-6B93-4E2A-85C8-2597DCAA1E46}");

      public static readonly ID Status = new ID("{253A74A4-D46E-49D7-B81B-F26249FC4A25}");

      public static readonly ID Duration = new ID("{D543D31D-BA5C-4E3B-BD86-4E60F2B8FED8}");

      public static readonly ID PreviewImageUrl = new ID("{AE745A50-3C98-4EB3-A5F3-F6885999357A}");

      public static readonly ID Player = new ID("{6619DFDB-A4A7-41FA-86EA-42CFBB6F25C4}");

      public static readonly ID CreatedAt = new ID("{BBF1A3FE-34F4-47D2-A1DD-B63F33BD5FE2}");

      public static readonly ID UpdatedAt = new ID("{49BB6380-FDD0-4FFA-B928-223BD907D77B}");

      public static readonly ID CustomMetadata = new ID("{0F5A8205-855A-4E48-857D-610EE750CBC9}");

      public static readonly ID LabelsList = new ID("{6EF3D739-AB34-4408-9387-EAFCE837462C}");

      public static readonly ID AssetType = new ID("{7DEB1E60-3073-4270-9C26-3F1562E2CCA6}");

      public static readonly ID IsLiveStream = new ID("{DAB35E1F-3F6A-4D0D-BED5-15AA6C258C10}");

      public static class Video
      {
        public static readonly ID OriginalFileName = new ID("{D5BBDD49-1388-47A1-B6A1-17062C11B306}");
      }

      public static class Channel
      {
        public static readonly ID VideoList = new ID("{3A79095D-E764-4DF3-A266-68A8163FF676}");
      }
    }

    public static class Label
    {
      public static readonly ID Id = new ID("{349094FF-1F64-479F-A6B3-64183067C1DD}");

      public static readonly ID Name = new ID("{9CCD5EDF-1E41-4275-8113-145ECE72A8BE}");

      public static readonly ID FullName = new ID("{BD8AC226-A239-4E53-8191-E8BD240C6698}");   
    }

    public static class Player
    {
      public static readonly ID Id = new ID("{32D339E7-1556-4589-8502-A4D63CC1F26D}");

      public static readonly ID Name = new ID("{C40A0145-FA84-480F-97A5-9C5B60FDE429}");

      public static readonly ID IsDefault = new ID("{7F63AA22-06A4-4B33-B0C8-7E0E480B5FDD}");

      public static readonly ID DefaultLanguage = new ID("{90DCB055-03E2-4D7F-821B-2822200DD33D}");

      public static readonly ID ProviderHomepageUrl = new ID("{4ED75DF1-6910-43F8-AFCF-C961FA9EBE97}");

      // Branding  

      public static readonly ID AccentColor = new ID("{C787E7B2-C8DB-46EE-B4C1-1D55E10EF064}");

      public static readonly ID ShowInfoScreenHomepageLink = new ID("{EB6ED6C1-6697-4120-937B-D7CF564E91C5}");

      public static readonly ID ShowShareButton = new ID("{A5178F56-A37C-4630-A452-91AD65E14AEC}");

      public static readonly ID TwitterSharing = new ID("{DBF61CDA-B2D8-4DDC-BAF4-52A667A179F9}");

      public static readonly ID ShowAdCountdown = new ID("{C03FECDB-062F-4CD3-A460-D99511F090F6}");

      public static readonly ID ShowInfoScreenTitle = new ID("{9C98AE88-4374-4C3F-B82E-2F40AD68A7D9}");

      public static readonly ID ShowInfoButton = new ID("{2FEE2035-70B6-412F-ACE3-4F1DB51C727A}");

      public static readonly ID UrlSharing = new ID("{336185F0-23E0-4EA1-BF9D-8516ED0448B4}");

      public static readonly ID FacebookSharing = new ID("{52467BF6-4E4A-494C-8C0A-917D2676491B}");

      public static readonly ID EmailSharing = new ID("{D93974F3-FE33-41C5-8E74-7FAD1FBEFA9A}");

      public static readonly ID ShowInfoScreenDescription = new ID("{E2ACB175-51AA-4849-ADB0-ADD0610A8D0C}");

      public static readonly ID ShowBitrateButton = new ID("{D29ED616-4F45-4ED0-8840-EA119DAF945B}");

      public static readonly ID DiggSharing = new ID("{C28ECFE5-61D1-4F12-B6F5-D489855D4359}");

      public static readonly ID ShowEmbedButton = new ID("{FF8C4E39-331D-4461-A313-E0D9A7DE290E}");

      public static readonly ID ShowChannelButton = new ID("{8AC514E7-7AD7-4823-AE49-011E01FBCF07}");

      public static readonly ID ShowEndScreenReplayButton = new ID("{9604E970-90B3-439E-9AA0-A87F1357994A}");

      public static readonly ID EnableErrorScreen = new ID("{1202A817-25E7-40ED-A53B-AB0FFB5B4522}");

      public static readonly ID ShowVolumeButton = new ID("{AD82A501-2FF8-4A88-8CE0-4C9A6F44A8F8}");

      public static readonly ID Chromeless = new ID("{1FC61563-E41F-4BBE-B127-EBA59C04C4D3}");
      
      // Scrubber
      public static readonly ID AlwaysShow = new ID("{3511FF3A-2BAD-4DCE-81F9-676A4C1D8B61}");

      public static readonly ID ScrubberImageUrl = new ID("{2B529FAD-1A64-4824-9550-0E74592F2FC5}");

      // Playback
      public static readonly ID BufferOnPause = new ID("{0E47CCC4-72AD-4807-9D1A-77ADB3F16F04}");

      // Watermark
      public static readonly ID WatermarkImageUrl = new ID("{55131A0D-4902-4193-A3E4-982B693CFFD2}");

      public static readonly ID ClickUrl = new ID("{6515C140-9473-44E3-BA5B-8F8989E4098E}");

      public static readonly ID Alpha = new ID("{B8C6DF75-DFA7-44F4-8324-38263630454B}");

      public static readonly ID Parameters = new ID("{C17D63B0-E9C0-4067-A307-1BECC635CA3B}"); 
    }
  }
}