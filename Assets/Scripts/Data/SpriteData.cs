using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteData
{
    //모든 스프라이트 데이터 인덱싱(파일 경로-스프라이트 파일 연결)
    public static Dictionary<string, Sprite> SpriteDictionary { get; set; } = new Dictionary<string, Sprite>();

}
