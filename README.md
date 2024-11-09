# Chronosphere - Dynamiczna gra platformowa 3D

🚀 Zanurz się w pulsującym energią świecie Chronosphere! Ta innowacyjna gra platformowa 3D łączy wymagającą rozgrywkę z fascynującą mechaniką manipulacji czasem, grawitacją sferyczną i dynamicznie generowanymi poziomami. Wciel się w Chrononautu i wyrusz na niezapomnianą przygodę.

🌌 Wykorzystaj swoje moce kontrolowania czasu, staw czoła nowym przeciwnikom, zbieraj punkty i doskonal swoje umiejętności, aby odkryć tajemnice Chronosphere.

## ✨ Wersja 1.2 - Chrononaut kontra Łowcy Czasu!

Aktualizacja 1.2 to zastrzyk świeżej rozgrywki!  Nowi przeciwnicy, system punktacji, ulepszony ruch i rozbudowane poziomy czekają na Ciebie.  Przygotuj się na dynamiczne starcia i sprawdź swoje umiejętności.


## 🚀 Nowe Funkcjonalności (1.2):

* **👾 Przeciwnicy:**  Dwa nowe typy przeciwników:
    * **Strażnik (Common Enemy):** Patroluje, atakuje w zwarciu.
    * **Łowca (Mini-boss):** Atak dystansowy, teleportacja.

* **⭐ System Punktacji:** Zdobywaj punkty za pokonywanie przeciwników (100 za Strażnika, 500 za Łowcę). Wynik w lewym górnym rogu ekranu.

* **🤸 Podwójny Skok:**  Dodatkowy skok w powietrzu (Spacja).

* **⚙️ Rozszerzone Generowanie Poziomów:**
    * Nowe chunków: ruchoma platforma, obracające się przeszkody, skoki przez przepaść.
    * Ulepszony algorytm generowania dla większej różnorodności.

* **🛠️ Optymalizacja:** Object pooling dla chunków i pocisków, redukcja draw calls.


## 🕰️ Manipulacja Czasem:

* **Spowolnienie (T):** Spowalnia czas, kosztuje energię.
* **Cofanie (R):** Cofnij się w czasie, kosztuje dużo energii.

## ⚡ Energia Czasowa:

Regeneruje się z czasem. Używaj mądrze!

## 🌍 Grawitacja Sferyczna:

Grawitacja działa w kierunku centrum planety.


## 📂 Struktura Projektu (C#):

* **PlayerController.cs:** Ruch gracza (podwójny skok).
* **TimeManager.cs:** Manipulacja czasem.
* **GameManager.cs:** Menu, ustawienia, kamera, punktacja.
* **LevelGenerator.cs:** Generowanie poziomów.
* **EnemyAI.cs:**  AI przeciwników.
* **Projectile.cs:** Pociski.


## 🛠️ Implementacja w Unity - Dla Programisty:

1. **Dodaj zasoby (modele, tekstury, animacje, dźwięki).**  *Pamiętaj o stworzeniu modeli i animacji dla Strażnika i Łowcy,  a także  modelu  pocisku  dla  Łowcy.*
2. **Skonfiguruj sceny.**  *Dodaj  prefabrykaty przeciwników na  scenie.  Ustaw  punkty  patrolowe  dla  każdego  przeciwnika  w  skrypcie  `EnemyAI`.*  *Skonfiguruj UI, w  tym  tekst  wyświetlający  wynik,  i  przypisz  go  do  zmiennej  `scoreText`  w  skrypcie  `GameManager`.*
3. **Stwórz prefabrykaty chunków poziomów (w  tym  nowe  chunków  z  1.2).** *Upewnij się, że  prefabrykaty  dla  ruchomych  platform,  obracających  się  przeszkód  i  skoków  przez  przepaść  są  poprawnie  skonfigurowane  z  animacjami/skryptami.*
4. **Ustaw warstwę "Ground".** *Upewnij  się,  że  wszystkie  obiekty,  po  których  gracz  może  chodzić,  są  przypisane  do  warstwy  "Ground".*
5. **Skonfiguruj planetę i centrum grawitacji.** *Ustaw  referencję  do  obiektu  reprezentującego  centrum  planety  w  skrypcie  `PlayerController`.*
6. **Stwórz  prefabrykat  pocisku  i  przypisz  go  do  `projectilePrefab` w  skrypcie  `EnemyAI`  dla  mini-bossa  "Łowca".**
7. **Dodaj skrypt  `PlayerHealth`  do  gracza  z  metodą  `TakeDamage(int damage)`.** *Ten  skrypt  będzie  odpowiadał  za  odejmowanie  punktów  życia  gracza  po  otrzymaniu  obrażeń.*
8. **Przetestuj i dopracuj grę.** *Zwróć  szczególną  uwagę  na  balans  gry  (statystyki  przeciwników,  ilość  zdobywanych  punktów)  oraz  płynność  działania.*



## 💻 Technologie:

* Unity
* C#

## 📜 Licencja:

MIT License - CatDeveloper

## ✉️ Kontakt:

CatDeveloper - bosyjjakub@gmail.com 
