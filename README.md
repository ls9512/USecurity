# USecurity

**USecurity** is a security component used in the **Unity** project. It provides encryption of commonly used data types at runtime, **PlayerPrefs** storage encryption, and a quick call interface for encryption and decryption of common encryption algorithms.

![topLanguage](https://img.shields.io/github/languages/top/ls9512/USecurity)
![size](https://img.shields.io/github/languages/code-size/ls9512/USecurity)
![issue](https://img.shields.io/github/issues/ls9512/USecurity)
![license](https://img.shields.io/github/license/ls9512/USecurity)
![last](https://img.shields.io/github/last-commit/ls9512/USecurity)
[![996.icu](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu)

[[中文文档]](README_CN.md)

<!-- vscode-markdown-toc -->
* 1. [Quick Start](#QuickStart)
* 2. [Anti Cheat](#AntiCheat)
	* 2.1. [Anti Cheat Value - Runtime Data Encryption](#AntiCheatValue-RuntimeDataEncryption)
		* 2.1.1. [Introduction](#Introduction)
		* 2.1.2. [C# Type](#CType)
		* 2.1.3. [Unity Type](#UnityType)
		* 2.1.4. [Working Principle](#WorkingPrinciple)
		* 2.1.5. [How To Use](#HowToUse)
		* 2.1.6. [Attention](#Attention)
	* 2.2. [PlayerPrefsAES](#PlayerPrefsAES)
		* 2.2.1. [Introduction](#Introduction-1)
		* 2.2.2. [How To Use](#HowToUse-1)
* 3. [DFA - Sensitive Word Filtering](#DFA-SensitiveWordFiltering)
	* 3.1. [Introduction](#Introduction-1)
	* 3.2. [How To Use](#HowToUse-1)
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

##  1. <a name='QuickStart'></a>Quick Start
1. Copy the folder to the `UnityProject/Assets/Plugins/` directory.
2. Set the key file in the `Resources/` subdirectory of each type of encryption needed under the `EncDec/` directory, and set the encryption key you want, or click `Create Key` in the component menu to generate a random key.
3. If the project does not use the `Resources.Load` method to load resources, you can adjust the storage path of the key configuration file by yourself and replace the interface implementation of `USecurityInterface.Load`, or you can do secondary encryption on the key resource file.

##  2. <a name='AntiCheat'></a>Anti Cheat

###  2.1. <a name='AntiCheatValue-RuntimeDataEncryption'></a>Anti Cheat Value - Runtime Data Encryption

####  2.1.1. <a name='Introduction'></a>Introduction
**AntiCheatValue** is the core function of the **USecurity** component. It provides runtime encrypted data types corresponding to the native data types of C# and Unity. In most cases, these data types can directly replace the original native data in the project. Type, and use the same style and method as the original type, which can be efficiently accessed so that existing projects can quickly obtain runtime anti-cheating capabilities.

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

Provides most commonly used C# data types, and all implement the interfaces of `IComparable`, `IFormattable`, `IConvertible`, `IEquatable`, `IFormattable`, `IEnumerable`, `ICloneable`, etc. contained in the native type, and implement `ISerializable` `Interface to support serialized storage. All types implement **implicit conversion** and common **operator overloading** with corresponding primitive types.

####  2.1.3. <a name='UnityType'></a>Unity Type
|Anti Cheat Type|Source Tyoe|
|-|-|
|cColor|Color|
|cQuaternion|Quaternion|
|cVector2|Vector2|
|cVector3|Vector3|
|cVector4|Vector4|

The Unity encrypted data type is a secondary encapsulation based on the C# encryption type. For example, **cVector3** essentially encapsulates 3 **cFloat**, so it can be combined with actual needs to expand other complex encrypted data types.

####  2.1.4. <a name='WorkingPrinciple'></a>Working Principle
By encapsulating the native type, the upper-level business logic accesses and manipulates the encapsulated type without directly operating the actual internal value. When the encapsulation type is operated, the real value passed in will be processed through encryption algorithms, memory offset, etc., and the processing methods for different data types are different. It can effectively avoid conventional cheating methods for searching and locating memory data.

####  2.1.5. <a name='HowToUse'></a>How To Use
```cs
// Directly replace the native type
public cInt num = 1;

// Mutual assignment operations with native types
public int a = 1;
public cInt b = a;
a = b;

// Perform regular mathematical operations directly with native types
public int left = 1;
public cInt right = 1;
right++;
public int add = left + right;

// Routine mathematical operations between different encryption types
public cInt num1 = 1;
public cFloat num2 = 1f;
float result = num1 * num2;

// Compare with native type value
public cInt i1 = 1;
public int i2 = 1;
public cInt i3 = 1;
public Debug.Log(i1 == i2);
public Debug.Log(i1 == i3);

// Support using the Convert interface for type conversion
public cInt srcValue = 1;
public int dstValue = Convert.ToInt32(srcValue);
```

####  2.1.6. <a name='Attention'></a>Attention
* Since the dynamic encryption key is regenerated every time it runs, the same actual value will be different every time it runs, so the internal value should not be used for storage.
* Encryption operations have a certain performance overhead. Use simple numeric encryption types as much as possible and apply only to important data as much as possible.

###  2.2. <a name='PlayerPrefsAES'></a>PlayerPrefsAES

####  2.2.1. <a name='Introduction-1'></a>Introduction
Provides **AES** encryption and decryption package for **PlayerPrefs** archive function, the style is consistent with the native interface, and some additional access interfaces for commonly used data types are added, and **PlayerPrefs** can be used as the main storage Directly replace in the project of the method to quickly obtain storage encryption capabilities.

####  2.2.2. <a name='HowToUse-1'></a>How To Use
```cs
// Save value
PlayerPrefsAES.SetInt("Key", 100);

// Load value
var value = PlayerPrefsAES.GetInt("Key");

// Delete value
PlayerPrefsAES.DeleteKey("Key");

// Delete all data
PlayerPrefsAES.DeleteAll();

// Save & Write data
PlayerPrefsAES.Save();
```

***

##  3. <a name='DFA-SensitiveWordFiltering'></a>DFA - Sensitive Word Filtering

###  3.1. <a name='Introduction-1'></a>Introduction
**DFA** is the full name **Deterministic Finite Automaton**, which is a more commonly used sensitive word filtering algorithm. This component compares and filters the user text input in the project with the sensitive word dictionary.

###  3.2. <a name='HowToUse-1'></a>How To Use
```cs
// Set to replace sensitive characters
DFAUtil.ReplaceSymbol = "*";

// Set the thesaurus file separator
DFAUtil.Separator = new[] { ",", "\n", "\r" };

// Initialize with pre-prepared thesaurus text
var text = Resources.Load<TextAsset>("DFA").text;
DFAUtil.Init(text);

// Filter input content
var content = ".....";
var result = DFAUtil.FilterWords(content, out var isLimit);
Debug.Log(isLimit);
Debug.Log(result);
```

***

##  4. <a name='EncryptDecrypt'></a>Encrypt / Decrypt

###  4.1. <a name='AES'></a>AES
Provides AES encryption and decryption of strings and files, with faster speed and higher security.

###  4.2. <a name='DES'></a>DES
Provides DES encryption and decryption of strings and files, which is slower and less secure.

###  4.3. <a name='RC4'></a>RC4
Provide RC4 encryption and decryption of strings, the fastest speed, security is not guaranteed, and the password can be variable length.

###  4.4. <a name='RSA'></a>RSA
It provides RSA signature and verification, encryption and decryption of strings and files, which is slower and has the highest security. The password is generated by the program.

###  4.5. <a name='Base64'></a>Base64
Provide base64 encoding and decoding of text.

***

##  5. <a name='MD5'></a>MD5
Provides standard MD5 value and short MD5 value calculation for text and files.