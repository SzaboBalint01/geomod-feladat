# Geometriai modellezés

## Rövid leírás

A feladat egy egyszerű Bézier görbe szerkesztő alkalmazás megvalósítása .NET környezetben.
A program lehetővé teszi a felhasználóknak Bézier görbék létrehozását, szerkesztését és megjelenítését.
A program a de Casteljau algoritmust használja a Bézier görbe pontjainak kiszámításához. 

## Funkciók leírása

### Kontrollpontok hozzáadása
Az üres rajzterületre kattintáskor egy kontrollpont jön létre.
Minden kontrollpont egy piros körként jelenik meg mellette a pont számával (P0, P1, stb.)

### Kontrollpontok mozgatása
A már meglévő kontrollpontok helyzete az egér kattintás közbeni mozgatással változtatható.
A görbe valós időben frissül a pontok mozgatásakor.

### Görbe megjelenítése
A kék folytonos vonal a Bézier görbét jelzi.
A szürke szaggatott vonal a kontrollpontokat összekötő segédvonalat mutatja.

### Pontok törlése
A "Clear" gombra kattintva az összes pont törlésre kerül.
