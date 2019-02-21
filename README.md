# LocCheck

A GitHub App that blocks pull requests with changes to .xlf files.

## Design

- If a pull request targets an _unprotected_ branch then it is marked as green, regardless of any changes to .xlf files.
- If a pull request targets a _protected_ branch:
  - If it does not modify any .xlf files it is marked as green.
  - If it does add, remove, or modify .xlf files it is marked as red.
- A repo administrator can explicitly allow a blocked PR (i.e. force it to green) with a comment. This allows pull requests from translation teams when you wouldn't otherwise want localization changes.

