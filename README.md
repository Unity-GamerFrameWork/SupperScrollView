# 无限滚动解决方案
## 介绍
1. 什么是无限滚动?
<br>无限滚动是指滚动列表中的Item对象，支持无限回收在利用的循环滚动列表。
2. 作用
<br>利用对象回收利用的机制，提升在进行大批量数据滚动显示时的性能。
## 核心API
```InitListView```:初始化滚动列表
<img width="1423" height="559" alt="image" src="https://github.com/user-attachments/assets/a3879397-18ca-4f04-aceb-4a76f618e664" />

```SetListItemCount```:重新设置滚动列表个数:数据增减必须调用此接口,否则会出现item索引与数据不一致和一起其他显示的Bug
<img width="983" height="187" alt="image" src="https://github.com/user-attachments/assets/51ebb5d0-2eeb-4658-90c8-2d8dee035f36" />

