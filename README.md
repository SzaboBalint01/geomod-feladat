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

## Változások

### **Javítva**: A görbe magasabb fokszám esetén nem mindig ér el az utolsó kontrollponthoz

**Probléma**: Magasabb fokszámok esetén az eredeti ciklus utolsó iterációjánál a `t` értéke gyakran több volt, mint `1.0`. Ezt a `t += 1.0/STEPS` művelet okozta.

**Megoldás**: Egész szám alapú iterációra cseréltem (`for (int i = 1; i <= STEPS; i++)`), így a görbe mindig pontosan eléri a `t = 1.0` értéket és megfelelően csatlakozik az utolsó kontrollponthoz.

```csharp
// Eredeti
for (double t = 0; t <= 1.0; t += 1.0 / STEPS)

// Javított
for (int i = 1; i <= STEPS; i++)
{
   double t = (double)i / STEPS;
   // ...
}