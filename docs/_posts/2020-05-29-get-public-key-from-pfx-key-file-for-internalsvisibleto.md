---
title: Get Public Key from PFX Key File for InternalsVisibleTo
layout: post
date: '2020-05-29 06:45:03'
categories:
- development
---

**If you want just to obtain public key from PFK file then directly go to the command lines far below.**

Public key is used by unit test to access internal classes or properties from other assembly.
The assembly to be accessed must add the following code in project AssemblyInfo.cs file:

```
[assembly: InternalsVisibleTo("VideoBrowser.Test, PublicKey=00240000048000009400000006020000002400005253413100040000010001003d454c10e8118a" +
    "f6e3ae8b517e05c1ad188b072aafdd4b131e3984e5fca8ebda5793ef5aac6a16d210883d806f2d" +
    "23c27a01896da983941821c6d639be111795ff4ea57f2de9bd5790a52554cba2bcc9446d53e55d" +
    "696f5e37904f79e6750dac1a0676a1cf2a73f7ea9c0418f1bca7fabc1be09df2fdd283b58f8539" +
    "8a79f8b1")]
```

VideoBrowser.Test is the name of the unit test or other project which want an access for this project or assembly like the unit test.<br/>
PublicKey is the public key of the project to be accessed, it is in the SNK file, but sometime we get PFX key instead from Visual Studio.<br/>
Therefore we must obtain the public key from PFK file by:
1. Converting PFK file it to snk.
2. Get public key from snk file by producing txt file from snk

# Command Lines
You can use full path of sn.exe or set the path of sn.exe in your Path of System Environment Variable Path in Window settings.<br/>
In my computer it is installed by Visual Studio in: C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7.2 Tools\x64.<br/>
In this example, the PFX file name is key.pfx.

**Convert PFX to SNK file**

```
sn.exe -i  key.pfx Personal
```

The following question will appear:

> Microsoft (R) .NET Framework Strong Name Utility  Version 4.0.30319.0<br/>
> Copyright (c) Microsoft Corporation.  All rights reserved.<br/>
> 
> Enter the password for the PKCS#12 key file:

Enter the PFX password which is given when you created it.<br/>
If correct then will show a message: Key pair installed into 'Personal'.

Type the following code to producekey. snk file.
```
sn.exe -pc Personal key.snk
```

> Microsoft (R) .NET Framework Strong Name Utility  Version 4.0.30319.0
> Copyright (c) Microsoft Corporation.  All rights reserved.
> 
> Public key written to key.snk
> 

A key.snk file will be created.

**Show or extract the public key of key.snk file to a file text key.txt**

```
sn.exe -tp key.snk > key.txt
```

or the following to show the public key without creating a text file.

```
sn.exe -tp key.snk
```

The key.txt contains public key token:

> Microsoft (R) .NET Framework Strong Name Utility  Version 4.0.30319.0
> Copyright (c) Microsoft Corporation.  All rights reserved.
> 
> Public key (hash algorithm: sha1):
> 00240000048000009400000006020000002400005253413100040000010001003d454c10e8118a
> f6e3ae8b517e05c1ad188b072aafdd4b131e3984e5fca8ebda5793ef5aac6a16d210883d806f2d
> 23c27a01896da983941821c6d639be111795ff4ea57f2de9bd5790a52554cba2bcc9446d53e55d
> 696f5e37904f79e6750dac1a0676a1cf2a73f7ea9c0418f1bca7fabc1be09df2fdd283b58f8539
> 8a79f8b1
> 
> Public key token is 52664285ede97425


And we can put the Public key,  but not the public key token in the InternalsVisibleTo command like above.<br/>
After recompiling VideoBrowser and VideoBrowser.Test then VideoBrowser.Test project classes can access internal member of VideoBrowser.
