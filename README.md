#### Краткое описание

Прототип глобальной стратегии в средневековом сеттинге. Мой текущий Pet project. 
На данной момент включает в себя:
 - два варианта стратегической карты: сделаный вручную и сгенерированный на основе карты высот с разными механиками перемещения армий
 - конструктор юнитов и его взаимодействие с другими механиками: технологиями и строительством
 - систему шпионажа
 - прототип боевой системы
 - генератор карт для сражений

#### О коде
 - для связи классов используются scriptableobject'ы. Использование паттерна Singleton, статических свойств и методов сведено к минимуму.
 - небольшая база данных на scriptableobject'ах
 - реализован алгоритм поиска пути А*
 - довольно продвинутый вариант State machine, задающий поведения отрядов ИИ и игрока
 - версия Unity: 2020.3.18

#### Скриншоты

Инспекторы:
<img src="screens/custom_inspector_double.png" width="600">

Пример генерации карты:
<img src="screens/global_map_2.png" width="600">

Прототип боевой системы:
<img src="screens/battle_screen.png" width="600">

Конструктор отрядов:
<img src="screens/unit_editor_1.png" width="600">

Конструктор названий для отрядов:
<img src="screens/unit_editor_2.png" width="600">

Вариант глобальной карты, с которого начался прототип:
<img src="screens/global_map.png" width="600">