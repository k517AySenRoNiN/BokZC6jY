// 代码生成时间: 2025-09-22 12:07:03
using System;
using System.IO;
using System.Xml.Linq;
using System.Configuration;

// ConfigFileManager.cs
// 该类用于管理配置文件，提供读取和更新配置项的功能。
public class ConfigFileManager
{
    private const string ConfigFileName = "App.config"; // 配置文件名
    private readonly string configFilePath;

    // 构造函数，初始化配置文件路径
    public ConfigFileManager()
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        configFilePath = Path.Combine(currentDirectory, ConfigFileName);
    }

    // 读取配置项
    public string ReadConfiguration(string key)
    {
        try
        {
            XDocument config = XDocument.Load(configFilePath);
            var setting = config.Descendants("appSettings")
                .Elements("add")
                .FirstOrDefault(el => el.Attribute("key