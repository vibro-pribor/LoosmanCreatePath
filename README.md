# LoosmanCreatePath
Плагин для Лоцман:PLM. Создаёт путь согласно дереву Лоцман
Плагин может  быть  установлен  как индивидуально, так и централизовано.
У плагина имеется два файла конфигурации. Они распологаются в домашней папке пользователя.
Возможно  файл  с настройками перенести на сервер,  и указать  путь к нему.
Файл  **PathSettings.vpxml** содержить настройку указания пути до основного  файла конфигурации
```xml
<?xml version="1.0" encoding="utf-8"?>
<PathsSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <PathMainSettings> [Путь до сервера]\MainSettings.vpxml</PathMainSettings>
</PathsSettings>
```
Файл **MainSettings.vpxml** содержит основные настройки работы с деревом Лоцман
В секции  *ProcessedTypes* указываются типы Лоцман для которых может создаваться папка
```xml
 <ProcessedTypes>
    <string>папка</string>
    <string>...</string>    
    <string>Сборочная единица</string>
  </ProcessedTypes>
```
  В секции *ReplaceablePaths* уакзываются все пути которые были созданы, и при  повторном создании данные не будут запрашиваться у пользователя, а сразу будет создан необходимый путь в файловой системе  
  
  ```xml
   <ReplaceablePaths>
    <item>
      <!--key - путь в дереве лоцман  -->
      <key>
        <string>;Изделия основного производства;Датчики;Датчики вибрации;АБВГ.433642.023 - 58;АБВГ.433642.023 - КД</string>
      </key>
      <!--key - путь в файловой системе  -->
      <value>
        <string>Изделия основного производства\Датчики\Датчики вибрации\АБВГ.433642.023 - МВ-58</string>
      </value>
    </item>
    </ReplaceablePaths>
```
 
  
  В секции  *ReplaceableSymbols* указываются специальные символы используемые в Лоцман которые необходимо заменить при  создании папок, например символ **№** заменить на **_**
  ```xml
   <ReplaceableSymbols>
    <item>
      <key>
        <string>!</string>
      </key>
      <value>
        <string>_</string>
      </value>
    </item>
    <item>
    </ReplaceableSymbols>
   ```
    Пример  использования плагина приведён на рисунке.
    
![2021-12-15_12-23-08](https://user-images.githubusercontent.com/20474616/146159308-29fd79a7-621a-4a96-93a1-204707041041.png)
