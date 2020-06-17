---
layout: home
---

<p align="center">
   <img src="{{site.image_folder}}cekli-logo.png"><br/>
   <strong>Cekli Browser and Youtube Downloader</strong>
</p>
Master [![Build Status](https://dev.azure.com/cekli/cekli/_apis/build/status/yoyokits.VideoBrowser?branchName=master)](https://dev.azure.com/cekli/cekli/_build/latest?definitionId=3&branchName=master)
Development [![Build Status](https://dev.azure.com/cekli/cekli/_apis/build/status/yoyokits.VideoBrowser?branchName=development)](https://dev.azure.com/cekli/cekli/_build/latest?definitionId=3&branchName=development)

Free Internet browser and Youtube video downloader Windows desktop application.
The video quality is customizable and can be downloaded in multithreading.
The software is created as simple as possible like using other web browsers like Google Chrome, Internet Explorer, or Firefox.

Please note that this is in early development, therefore you may find bugs. <br/>
Bug reports or following the development progress can be reached via: 

{{site.contact}}

![Cekli Video Browser Screenshot]({{site.image_folder}}/{{site.version}}/CekliVideoBrowserDownloadOptionsScreenShot.jpg)

{{site.title}} based on [youtube-dl][youtube-dl] therefore in future it can support more than 100 other video websites like:
* youtube -> supported
* --- will be supported in the future ---
*  Bloomberg
*  BusinessInsider
*  CartoonNetwork
*  CNN
*  dailymotion
*  facebook
*  Flickr
*  mtv
* The complete supported sites is [youtube-dl supported sites][youtube-dl-supported-sites]

**Download**

The last version is {{site.version}}.<br/>
And the most recent installer can be downloaded here:

[![Download]({{site.image_folder}}Download.jpg)](download.html)

The releases history can be followed in:

[Release Notes](release-notes.html)

It has dependencies (usually already installed in your system)  with:
* At least .Net Framework 4.6.2
* Visual C++ 2015 Redist

**Usage**

![{{site.title}} as standard browser]({{site.image_folder}}/{{site.version}}/CekliVideoBrowserMainViewScreenShot.jpg)
Like any other web browsers, {{site.title}} can be used as a web browser.

If we browse in a youtube video then the download icon will be enabled, after clicking then it will be downloaded in a queue.
It can downloads several video concurently or multithreading.

> **Warning:** some Video websites not allowing or limiting download number in a time, please make sure of it.

![Doownload Queue]({{site.image_folder}}/{{site.version}}/CekliVideoBrowserDownloadScreenShot.jpg)
Download queue

The url text input field can be used for youtube search if we type in non url format.

![Url search mode]({{site.image_folder}}/{{site.version}}/CekliVideoBrowserYoutubeSearchScreenShot.jpg)
Url text input as youtube search

**Support this project**

Simply click star to give me a power for further development of this project.

**Acknowledgement**

This software will not exist without the previous projects like:
{% include_relative /_includes/acknowledgement.md %}

**License**

[MIT license](about.html)<br/>
It means you can do whatever you want from copying, redistibuting and  modifying the source code.
Please use it wisely with your own risk.

[youtube-dl]: http://ytdl-org.github.io/youtube-dl/
[youtube-dl-supported-sites]: https://ytdl-org.github.io/youtube-dl/supportedsites.html
