using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZookeeperBrowser.Model.Enums
{
    /// <summary>
    /// 民族
    /// </summary>
    public enum EnumNation
    {
        /// <summary>
        /// 汉族
        /// </summary>
        [Description("汉族")]
        [Display(Name = "汉族")]
        hanzu,
        /// <summary>
        /// 蒙古族
        /// </summary>
        [Description("蒙古族")]
        [Display(Name = "蒙古族")]
        mengguzu,
        /// <summary>
        /// 回族
        /// </summary>
        [Description("回族")]
        [Display(Name = "回族")]
        huizu,
        /// <summary>
        /// 藏族
        /// </summary>
        [Description("藏族")]
        [Display(Name = "藏族")]
        zangzu,
        /// <summary>
        /// 维吾尔族
        /// </summary>
        [Description("维吾尔族")]
        [Display(Name = "维吾尔族")]
        weiwuerzu,
        /// <summary>
        /// 苗族
        /// </summary>
        [Description("苗族")]
        [Display(Name = "苗族")]
        miaozu,
        /// <summary>
        /// 彝族
        /// </summary>
        [Description("彝族")]
        [Display(Name = "彝族")]
        yizu,
        /// <summary>
        /// 壮族
        /// </summary>
        [Description("壮族")]
        [Display(Name = "壮族")]
        zhuangzu,
        /// <summary>
        /// 布依族
        /// </summary>
        [Description("布依族")]
        [Display(Name = "布依族")]
        buyizu,
        /// <summary>
        /// 朝鲜族
        /// </summary>
        [Description("朝鲜族")]
        [Display(Name = "朝鲜族")]
        chaoxianzu,
        /// <summary>
        /// 满族
        /// </summary>
        [Description("满族")]
        [Display(Name = "满族")]
        manzu,
        /// <summary>
        /// 侗族
        /// </summary>
        [Description("侗族")]
        [Display(Name = "侗族")]
        dongzu,
        /// <summary>
        /// 瑶族
        /// </summary>
        [Description("瑶族")]
        [Display(Name = "瑶族")]
        yaozu,
        /// <summary>
        /// 白族
        /// </summary>
        [Description("白族")]
        [Display(Name = "白族")]
        baizu,
        /// <summary>
        /// 土家族
        /// </summary>
        [Description("土家族")]
        [Display(Name = "土家族")]
        tujiazu,
        /// <summary>
        /// 哈尼族
        /// </summary>
        [Description("哈尼族")]
        [Display(Name = "哈尼族")]
        hanizu,
        /// <summary>
        /// 哈萨克族
        /// </summary>
        [Description("哈萨克族")]
        [Display(Name = "哈萨克族")]
        hasakezu,
        /// <summary>
        /// 傣族
        /// </summary>
        [Description("傣族")]
        [Display(Name = "傣族")]
        daizu,
        /// <summary>
        /// 黎族
        /// </summary>
        [Description("黎族")]
        [Display(Name = "黎族")]
        lizu,
        /// <summary>
        /// 僳僳族
        /// </summary>
        [Description("僳僳族")]
        [Display(Name = "僳僳族")]
        susuzu,
        /// <summary>
        /// 佤族
        /// </summary>
        [Description("佤族")]
        [Display(Name = "佤族")]
        wazu,
        /// <summary>
        /// 畲族
        /// </summary>
        [Description("畲族")]
        [Display(Name = "畲族")]
        shezu,
        /// <summary>
        /// 高山族
        /// </summary>
        [Description("高山族")]
        [Display(Name = "高山族")]
        gaoshanzu,
        /// <summary>
        /// 拉祜族
        /// </summary>
        [Description("拉祜族")]
        [Display(Name = "拉祜族")]
        lahuzu,
        /// <summary>
        /// 水族
        /// </summary>
        [Description("水族")]
        [Display(Name = "水族")]
        shuizu,
        /// <summary>
        /// 东乡族
        /// </summary>
        [Description("东乡族")]
        [Display(Name = "东乡族")]
        dongxiangzu,
        /// <summary>
        /// 纳西族
        /// </summary>
        [Description("纳西族")]
        [Display(Name = "纳西族")]
        naxizu,
        /// <summary>
        /// 景颇族
        /// </summary>
        [Description("景颇族")]
        [Display(Name = "景颇族")]
        jingpozu,
        /// <summary>
        /// 柯尔克孜族
        /// </summary>
        [Description("柯尔克孜族")]
        [Display(Name = "柯尔克孜族")]
        keerkezizu,
        /// <summary>
        /// 土族
        /// </summary>
        [Description("土族")]
        [Display(Name = "土族")]
        tuzu,
        /// <summary>
        /// 达斡尔族
        /// </summary>
        [Description("达斡尔族")]
        [Display(Name = "达斡尔族")]
        dawoerzu,
        /// <summary>
        /// 仫佬族
        /// </summary>
        [Description("仫佬族")]
        [Display(Name = "仫佬族")]
        molaozu,
        /// <summary>
        /// 羌族
        /// </summary>
        [Description("羌族")]
        [Display(Name = "羌族")]
        qiangzu,
        /// <summary>
        /// 布朗族
        /// </summary>
        [Description("布朗族")]
        [Display(Name = "布朗族")]
        bulangzu,
        /// <summary>
        /// 撒拉族
        /// </summary>
        [Description("撒拉族")]
        [Display(Name = "撒拉族")]
        salazu,
        /// <summary>
        /// 毛南族
        /// </summary>
        [Description("毛南族")]
        [Display(Name = "毛南族")]
        maonanzu,
        /// <summary>
        /// 仡佬族
        /// </summary>
        [Description("仡佬族")]
        [Display(Name = "仡佬族")]
        yilaozu,
        /// <summary>
        /// 锡伯族
        /// </summary>
        [Description("锡伯族")]
        [Display(Name = "锡伯族")]
        xibozu,
        /// <summary>
        /// 阿昌族
        /// </summary>
        [Description("阿昌族")]
        [Display(Name = "阿昌族")]
        achangzu,
        /// <summary>
        /// 普米族
        /// </summary>
        [Description("普米族")]
        [Display(Name = "普米族")]
        pumizu,
        /// <summary>
        /// 塔吉克族
        /// </summary>
        [Description("塔吉克族")]
        [Display(Name = "塔吉克族")]
        tajikezu,
        /// <summary>
        /// 怒族
        /// </summary>
        [Description("怒族")]
        [Display(Name = "怒族")]
        nuzu,
        /// <summary>
        /// 乌孜别克族
        /// </summary>
        [Description("乌孜别克族")]
        [Display(Name = "乌孜别克族")]
        wuzibiekezu,
        /// <summary>
        /// 俄罗斯族
        /// </summary>
        [Description("俄罗斯族")]
        [Display(Name = "俄罗斯族")]
        eluosizu,
        /// <summary>
        /// 鄂温克族
        /// </summary>
        [Description("鄂温克族")]
        [Display(Name = "鄂温克族")]
        ewenkezu,
        /// <summary>
        /// 德昂族
        /// </summary>
        [Description("德昂族")]
        [Display(Name = "德昂族")]
        deangzu,
        /// <summary>
        /// 保安族
        /// </summary>
        [Description("保安族")]
        [Display(Name = "保安族")]
        baoanzu,
        /// <summary>
        /// 裕固族
        /// </summary>
        [Description("裕固族")]
        [Display(Name = "裕固族")]
        yuguzu,
        /// <summary>
        /// 京族
        /// </summary>
        [Description("京族")]
        [Display(Name = "京族")]
        jingzu,
        /// <summary>
        /// 塔塔尔族
        /// </summary>
        [Description("塔塔尔族")]
        [Display(Name = "塔塔尔族")]
        tataerzu,
        /// <summary>
        /// 独龙族
        /// </summary>
        [Description("独龙族")]
        [Display(Name = "独龙族")]
        dulongzu,
        /// <summary>
        /// 鄂伦春族
        /// </summary>
        [Description("鄂伦春族")]
        [Display(Name = "鄂伦春族")]
        elunchunzu,
        /// <summary>
        /// 赫哲族
        /// </summary>
        [Description("赫哲族")]
        [Display(Name = "赫哲族")]
        hezhezu,
        /// <summary>
        /// 门巴族
        /// </summary>
        [Description("门巴族")]
        [Display(Name = "门巴族")]
        menbazu,
        /// <summary>
        /// 珞巴族
        /// </summary>
        [Description("珞巴族")]
        [Display(Name = "珞巴族")]
        luobazu,
        /// <summary>
        /// 基诺族
        /// </summary>
        [Description("基诺族")]
        [Display(Name = "基诺族")]
        jinuozu
    }
}
