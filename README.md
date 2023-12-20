# SkiaSharp.UDraw
## Introduction
Using SkiaSharp just like using Unity.
## Infomation
This project is still being written and could go through a lot of big changes.
## Usage
```csharp
var scene = new Scene();

var gameObject = new GameObject("Base");
var rectTransform = gameObject.AddComponent<RectTransform>();
rectTransform.pivot = Vector2.zero;
rectTransform.sizeDelta = rect.size;
gameObject.AddComponent<Canvas>();

scene.AddGameObject(gameObject);

var rect = new Rect(0, 0, 768, 1024);
using var resultImage = scene.Capture(rect);
```
## LICENSE
```txt
MIT License
Copyright (c) 2023 XeronOwO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

```
