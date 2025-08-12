# Casino Wallet

## Overview
The **Casino Wallet** is a simple C# console application that simulates a basic slot machine game.  
It manages the player's wallet balance, deposits, withdrawals, bets, and calculates winnings based on predefined game rules.

---
## Configuration (`appsettings.json`)

The application uses `appsettings.json` for configurable game settings:

```json
{
  "BetSettings": {
    "MinBet": 1,
    "MaxBet": 10
  },
  "GameSettings": {
    "LosePercent": 50,
    "WinUpTo2Percent": 40,
    "MaxSmallWinMultiplier": 2.0,
    "MaxBigWinMultiplier": 10.0
  }
}
```
### BetSettings
| Key     | Description                        | Example |
|---------|------------------------------------|---------|
| MinBet  | Minimum bet amount allowed         | 1       |
| MaxBet  | Maximum bet amount allowed         | 10      |

### GameSettings
| Key                  | Description                                                     | Example |
|----------------------|-----------------------------------------------------------------|---------|
| LosePercent          | Percentage chance to lose the bet                               | 50      |
| WinUpTo2Percent      | Percentage chance to win up to x2 the bet                       | 40      |
| MaxSmallWinMultiplier| Maximum multiplier for small wins (when within WinUpTo2Percent) | 2.0     |
| MaxBigWinMultiplier  | Maximum multiplier for big wins                                 | 10.0    |
