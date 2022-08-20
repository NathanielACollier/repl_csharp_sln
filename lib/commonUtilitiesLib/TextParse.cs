using System;

namespace commonUtilitiesLib;
public static class TextParse
{


    public static byte[] parseBase64ImageTag(string imageTagData)
    {
        string base64SectionKey = ";base64,";
        int base64StrIndex = imageTagData.IndexOf(base64SectionKey);

        if( base64StrIndex < 0)
        {
            throw new Exception("Image Tag data did not contain a base64 section");
        }

        string base64imageText = imageTagData.Substring(base64StrIndex + base64SectionKey.Length );

        byte[] imageData= Convert.FromBase64String(base64imageText);
        return imageData;
    }



}
