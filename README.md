# UnoCostAnalyzer

## Opis
Aplikacja służy do monitorowania i zarządzania codziennymi wydatkami użytkownika. Umożliwia dodawanie wydatków z podziałem na kategorie, takie jak zakupy czy jedzenie, z opcjonalnym opisem. Użytkownik może przeglądać wydatki według kategorii oraz w wybranych zakresach czasowych, co pozwala na łatwiejsze zarządzanie finansami osobistymi.

## Wymagania funkcjonalne
- **Dodawanie wydatków:**
  - Użytkownik może wprowadzić kwotę, tagi (np. jedzenie, rachunki) oraz opis wydatku.
- **Filtracja danych:**
  - Możliwość filtrowania wydatków według kategorii.
- **Wyświetlanie historii:**
  - Lista wydatków zawiera kwotę, opis, tagi oraz datę dodania.
- **Sumowanie wydatków:**
  - Automatyczne obliczanie sumy wszystkich wydatków dla wybranego zakresu czasowego i kategorii.
- **Edycja i usuwanie wydatków:**
  - Możliwość edycji lub usunięcia konkretnego wpisu z historii wydatków.
- **Interfejs użytkownika:**
  - Intuicyjne przyciski i pola tekstowe do wprowadzania i zarządzania wydatkami.

## Wymagania niefunkcjonalne
- **Responsywność:**
  - Aplikacja musi być responsywna, aby działać poprawnie na różnych rozdzielczościach ekranu.
- **Wydajność:**
  - System powinien działać płynnie przy zarządzaniu do 1000 rekordów.
- **Przyjazny interfejs:**
  - Prosty i intuicyjny interfejs, pozwalający na szybkie dodawanie i przeglądanie danych.
- **Bezpieczeństwo:**
  - Dane użytkownika powinny być przechowywane lokalnie lub w bezpiecznej bazie danych.
- **Niezawodność:**
  - Aplikacja musi działać stabilnie i nie powodować utraty danych w przypadku awarii.

## Potencjalne ryzyka
- **Nieznajomość technologii:**
  - Brak doświadczenia w pracy z bazami danych w C#.
- **Ograniczona wiedza na temat optymalizacji wydajności:**
  - Możliwe trudności przy obsłudze dużej liczby rekordów.
- **Problemy z UI/UX:**
  - Możliwość stworzenia interfejsu, który nie będzie intuicyjny dla użytkowników.
- **Błędy w obsłudze danych:**
  - Ryzyko nieprawidłowego przetwarzania lub wyświetlania danych z bazy.

