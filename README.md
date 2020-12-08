# USecurity

**USecurity** 是一个用于 **Unity** 项目的安全组件，提供运行时常用数据类型加密，**PlayerPrefs** 存储加密，以及常见加密算法的加密解密快调用接口。

![topLanguage](https://img.shields.io/github/languages/top/ls9512/USecurity)
![size](https://img.shields.io/github/languages/code-size/ls9512/USecurity)
![issue](https://img.shields.io/github/issues/ls9512/USecurity)
![license](https://img.shields.io/github/license/ls9512/USecurity)
![last](https://img.shields.io/github/last-commit/ls9512/USecurity)
[![996.icu](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu)

<!-- vscode-markdown-toc -->
* 1. [接入](#)
* 2. [Anti Cheat](#AntiCheat)
	* 2.1. [Anti Cheat Value 运行时数据加密](#AntiCheatValue)
		* 2.1.1. [简介](#-1)
		* 2.1.2. [C# Type](#CType)
		* 2.1.3. [Unity Type](#UnityType)
		* 2.1.4. [工作机制](#-1)
		* 2.1.5. [使用示例](#-1)
		* 2.1.6. [注意事项](#-1)
	* 2.2. [PlayerPrefsAES](#PlayerPrefsAES)
		* 2.2.1. [简介](#-1)
		* 2.2.2. [使用示例](#-1)
* 3. [DFA 敏感词过滤](#DFA)
	* 3.1. [简介](#-1)
	* 3.2. [使用示例](#-1)
* 4. [Encrypt / Decrypt](#EncryptDecrypt)
	* 4.1. [AES](#AES)
	* 4.2. [DES](#DES)
	* 4.3. [RC4](#RC4)
	* 4.4. [RSA](#RSA)
	* 4.5. [Base64](#Base64)
* 5. [MD5](#MD5)

<!-- vscode-markdown-toc-config
	numbering=true
	autoSave=true
	/vscode-markdown-toc-config -->
<!-- /vscode-markdown-toc -->

##  1. <a name=''></a>接入
1. 将整个文件夹复制到 `UnityProject/Assets/Plugins/` 目录下
2. 将 `EncDec/` 目录下，需要用到的每种加密类型的 `Resources/` 子目录下的 Key 文件，设置自己想要的加密密钥，或者点击组件菜单中的 `Create Key` 生成随机密钥。
3. 如果项目不使用 `Resources.Load` 方式加载资源，可以自行调整密钥配置文件的存储路径，并替换 `USecurityInterface.Load` 的接口实现，也可以对密钥资源文件做二次加密。

##  2. <a name='AntiCheat'></a>Anti Cheat

###  2.1. <a name='AntiCheatValue'></a>Anti Cheat Value 运行时数据加密

####  2.1.1. <a name='-1'></a>简介
**AntiCheatValue** 是 **USecurity** 组件的核心功能，提供大量鱼C#和Unity原生数据类型对应的运行时加密数据类型，在大多数情况下，这些数据类型可以直接替换项目中原有的原生类型，并保持与原生类型相同的风格和方法进行使用，可以高效接入使得现有项目快速获得反作弊能力。

####  2.1.2. <a name='CType'></a>C# Type
|Anti Cheat Type|Source Tyoe|
|-|-|
|cBool|bool|
|cByte|byte|
|cChar|char|
|cDecimal|decimal|
|cFloat|float|
|cInt|int|
|cLong|long|
|cShort|short|
|cString|string|

提供大多数常用C#数据类型，并且全部实现原生类型所包含的`IComparable`, `IFormattable`, `IConvertible`, `IEquatable`, `IFormattable`, `IEnumerable`, `ICloneable` 等接口，并且实现 `ISerializable` 接口以支持序列化存储。所有类型都实现了与对应原生类型的 **隐式转换** 和常用 **运算符重载** 。

####  2.1.3. <a name='UnityType'></a>Unity Type
|Anti Cheat Type|Source Tyoe|
|-|-|
|cColor|Color|
|cQuaternion|Quaternion|
|cVector2|Vector2|
|cVector3|Vector3|
|cVector4|Vector4|

Unity加密数据类型是基于C#加密类型的二次封装，例如 **cVector3** 本质上是封装了3个 **cFloat** ，因此可以结合实际需求，扩展出其他复杂加密数据类型。

####  2.1.4. <a name='-1'></a>工作机制
通过对原生类型进行封装，上层业务逻辑访问和操作封装后的类型而不直接操作内部真实的数值。在封装类型被操作时，传入的真实数值，会经过加密算法、内存偏移等方式进行处理，不同数据类型处理方式有所不同。可以有效规避常规的内存数据搜索定位作弊方式。

####  2.1.5. <a name='-1'></a>使用示例
```cs
// 直接替换原数据类型进行定义
public cInt num = 1;

// 与对应原生类型互相赋值操作
public int a = 1;
public cInt b = a;
a = b;

// 与原生类型直接进行常规数学运算
public int left = 1;
public cInt right = 1;
right++;
public int add = left + right;

// 不同加密类型之间进行常规数学运算
public cInt num1 = 1;
public cFloat num2 = 1f;
float result = num1 * num2;

// 与原生类型数值进行比较
public cInt i1 = 1;
public int i2 = 1;
public cInt i3 = 1;
public Debug.Log(i1 == i2);
public Debug.Log(i1 == i3);

// 支持被 Convert 做类型转换处理
public cInt srcValue = 1;
public int dstValue = Convert.ToInt32(srcValue);
```

####  2.1.6. <a name='-1'></a>注意事项
* 由于动态加密密钥每次运行都会重新生成，所以同样的实际数值每次运行内部数值都会不同，因此内部数值不应用于存储。
* 加密运算有一定性能开销，尽可能使用简单数值加密类型，尽可能只应用于重要数据。

###  2.2. <a name='PlayerPrefsAES'></a>PlayerPrefsAES

####  2.2.1. <a name='-1'></a>简介
提供对 **PlayerPrefs** 存档功能的 **AES** 加解密封装，风格与原生接口保持一致，并额外增加了一些常用数据类型的存取接口，可以在使用 **PlayerPrefs** 作为主要存储方式的项目中进行直接替换以快速获得存储加密能力。

####  2.2.2. <a name='-1'></a>使用示例
```cs
// 保存数值
PlayerPrefsAES.SetInt("Key", 100);

// 读取数值
var value = PlayerPrefsAES.GetInt("Key");

// 删除数值
PlayerPrefsAES.DeleteKey("Key");

// 删除所有数据
PlayerPrefsAES.DeleteAll();

// 保存数据
PlayerPrefsAES.Save();
```

***

##  3. <a name='DFA'></a>DFA 敏感词过滤

###  3.1. <a name='-1'></a>简介
**DFA** 全称 **Deterministic Finite Automaton** 确定有穷自动机，是比较常用的敏感词过滤算法。该组件对项目中的用户文字输入内容与敏感词词库进行比对并进行过滤。

###  3.2. <a name='-1'></a>使用示例
```cs
// 设置替换敏感字符
DFAUtil.ReplaceSymbol = "*";

// 设置词库文件分隔符
DFAUtil.Separator = new[] { ",", "\n", "\r" };

// 使用预先准备好的词库文本进行初始化
var text = Resources.Load<TextAsset>("DFA").text;
DFAUtil.Init(text);

// 过滤内容
var content = ".....";
var result = DFAUtil.FilterWords(content, out var isLimit);
Debug.Log(isLimit);
Debug.Log(result);
```

***

##  4. <a name='EncryptDecrypt'></a>Encrypt / Decrypt

###  4.1. <a name='AES'></a>AES
提供对字符串和文件的AES加密解密，速度较快，安全性较高。

###  4.2. <a name='DES'></a>DES
提供对字符串和文件的DES加密解密，速度较慢，安全性较低。

###  4.3. <a name='RC4'></a>RC4
提供对字符串的RC4加密解密，速度最快，安全性不做保证，密码可变长。

###  4.4. <a name='RSA'></a>RSA
提供对字符串和文件的RSA签名和验签、加密和解密，速度较慢，安全性最高，密码由程序生成。

###  4.5. <a name='Base64'></a>Base64
提供对文本的 base64 编码、解码。

***

##  5. <a name='MD5'></a>MD5
提供对文本、文件的标准MD5值和短MD5值计算。