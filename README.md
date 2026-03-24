# Uczelniana wypożyczalnia sprzętu

## Opis projektu
Projekt przedstawia konsolową aplikację w C# do obsługi uczelnianej wypożyczalni sprzętu.  
System pozwala na:
- dodawanie użytkowników,
- dodawanie sprzętu różnych typów,
- wypożyczanie sprzętu,
- zwrot sprzętu,
- naliczanie kar za opóźnienie,
- oznaczanie sprzętu jako niedostępnego,
- wyświetlanie aktywnych i przeterminowanych wypożyczeń,
- generowanie raportu podsumowującego.

## Wykorzystane elementy domenowe
W projekcie występują:
- wspólna klasa abstrakcyjna `Equipment`,
- typy sprzętu: `Laptop`, `Projector`, `Camera`,
- wspólna klasa abstrakcyjna `User`,
- typy użytkowników: `Student`, `Employee`,
- klasa `Rental` opisująca wypożyczenie,
- klasy serwisowe odpowiedzialne za logikę biznesową.

## Struktura projektu
- `Models` – model domenowy
- `Repositories` – przechowywanie danych w pamięci
- `Services` – logika biznesowa
- `Enums` – typy wyliczeniowe
- `Exceptions` – jawna obsługa błędów
- `Config` – reguły łatwe do zmiany

## Uzasadnienie podziału projektu
Podział został dobrany tak, aby oddzielić:
- dane domenowe,
- operacje biznesowe,
- warstwę uruchomieniową w `Program.cs`.

Dzięki temu logika systemu nie znajduje się w jednej klasie, a każda część projektu ma wyraźną odpowiedzialność.

## Kohezja, coupling i odpowiedzialności klas
Kohezja została zachowana przez to, że:
- klasy modelu przechowują dane domenowe,
- repozytoria odpowiadają za przechowywanie danych,
- serwisy wykonują operacje biznesowe.

Ograniczenie coupling widać w tym, że:
- `Program.cs` nie implementuje logiki wypożyczeń,
- reguły biznesowe nie są rozproszone po wielu klasach,
- kara za opóźnienie została umieszczona w `RentalRules`,
- limity wypożyczeń wynikają z typu użytkownika.

Odpowiedzialności klas:
- `Equipment` i `User` opisują wspólne cechy obiektów domenowych,
- `Rental` opisuje pojedyncze wypożyczenie,
- `RentalService` obsługuje wypożyczenia i zwroty,
- `EquipmentService` zarządza sprzętem,
- `UserService` zarządza użytkownikami,
- `ReportService` generuje raport końcowy.

## Obsługa wyjątków
Niepoprawne operacje są obsługiwane jawnie przez wyjątki:
- `EquipmentUnavailableException`
- `RentalLimitExceededException`

## Reguły biznesowe
- student może mieć maksymalnie 2 aktywne wypożyczenia,
- pracownik może mieć maksymalnie 5 aktywnych wypożyczeń,
- sprzęt niedostępny nie może zostać wypożyczony,
- opóźniony zwrot powoduje naliczenie kary.

## Instrukcja uruchomienia
1. Otworzyć projekt w Riderze.
2. Przywrócić zależności projektu.
3. Uruchomić aplikację przez `Program.cs`.
4. Program wykona scenariusz demonstracyjny i wyświetli wynik w konsoli.

## Scenariusz demonstracyjny
Program pokazuje:
- dodanie kilku egzemplarzy sprzętu,
- dodanie kilku użytkowników,
- poprawne wypożyczenie,
- próbę niepoprawnej operacji,
- zwrot w terminie,
- zwrot po terminie z karą,
- raport końcowy.