[![NuGet Downloads](https://img.shields.io/nuget/dt/CHaser_Connector?label=nuget%20Downloads&color=004880&logo=nuget&style=flat-square)](https://www.nuget.org/packages/CHaser_Connector/)  

## CHaser_Connector
CHaserのC#クライアント向け通信ライブラリです。  
CHaserサーバーとの通信機能を提供します。

C#でCHaserのコードを手っ取り早く書きたい方は[こちら](https://github.com/s1v/CHaser_CSharp_Template)のテンプレートをご利用ください。

## 免責事項
作者は本テンプレート・ライブラリに起因する損害の一切責任を負いません。  
バグなどを発見した場合はGitHubの[Issue](https://github.com/s1v/CHaser_Connector/issues)からお知らせください。

## リファレンス
[Wiki](https://github.com/s1v/CHaser_Connector/wiki)参照

## Usage
```C#
﻿using CHaserConnector;

Connector connector = new Connector(ip, port, name);
connector.Connect();

connector.GetReady();
connector.LookUp();
```
