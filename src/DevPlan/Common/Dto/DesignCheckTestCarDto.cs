﻿using System;
using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 試験車（設計チェック）検索入力モデルクラス
    /// </summary>
    public class DesignCheckTestCarGetInModel
    {
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }
        /// <summary>
        /// 登録済フラグ
        /// </summary>
        public bool? ENTRY_FLG { get; set; }
    }
    /// <summary>
    /// 試験車（設計チェック）検索出力モデルクラス
    /// </summary>
    public class DesignCheckTestCarGetOutModel
    {
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// GorT
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// 試験車名
        /// </summary>
        public string 試験車名 { get; set; }
        /// <summary>
        /// グレード
        /// </summary>
        public string グレード { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string 試験目的 { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string 車系 { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string 仕向地 { get; set; }
        /// <summary>
        /// 排気量
        /// </summary>
        public string 排気量 { get; set; }
        /// <summary>
        /// EG型式
        /// </summary>
        public string E_G型式 { get; set; }
        /// <summary>
        /// 駆動方式
        /// </summary>
        public string 駆動方式 { get; set; }
        /// <summary>
        /// T/M
        /// </summary>
        public string トランスミッション { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string 車型 { get; set; }
        /// <summary>
        /// 車体色
        /// </summary>
        public string 車体色 { get; set; }
    }
    /// <summary>
    /// 試験車（設計チェック）登録入力モデルクラス
    /// </summary>
    public class DesignCheckTestCarPostInModel
    {
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }
    }
    /// <summary>
    /// 試験車（設計チェック）更新入力モデルクラス
    /// </summary>
    public class DesignCheckTestCarPutInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }
    }
    /// <summary>
    /// 試験車（設計チェック）削除入力モデルクラス
    /// </summary>
    public class DesignCheckTestCarDeleteInModel
    {
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int ID { get; set; }
    }
}