# 🎵 BeatFighters  
A Turn-Based Rhythm Fighting Game (CSE 4400 Group Project)

BeatFighters is a competitive turn-based rhythm combat game where players create musical attack patterns and defend against their opponent’s rhythm. Each directional input represents both a drum sound and a physical fighting move. The result is a fast-paced musical duel where timing, creativity, and strategy determine the winner.

---

## 🕹️ Gameplay Overview

In BeatFighters, players alternate turns between **attacking** and **defending**:

### **Attacking**
- Players create a sequence of rhythmic directional notes.
- Each note produces both a drum sound and a fighter animation (jab, hook, kick, uppercut).
- The sequence becomes the attack combo the opponent must defend.

### **Defending**
- Players attempt to match the opponent’s notes *in rhythm*.
- **Perfect Timing** → no damage  
- **Good Timing** → reduced damage  
- **Missed Timing** → full damage  

### **Meters & Special Moves**
- **Offensive Meter:** Builds when creating notes or when the opponent misses.  
  - At full meter, players can unleash a **Crash Attack**, a powerful beat that deals heavy damage.

- **Defensive Meter:** Builds when blocking notes accurately.  
  - At full meter, players can **Interrupt** the opponent and cancel their turn, or block a Crash attack entirely.

### **Win Condition**
Reduce the opponent’s HP to zero OR have the highest HP when the song ends.

---

## 🎮 Controls

### **Player 1**
- `Q`, `W`, `E`, `R` → Notes / Attacks  
- `TAB` → Interrupt  
- `QWER` → Crash / Block  

### **Player 2**
- `O`, `P`, `[`, `]` → Notes / Attacks  
- `ENTER` → Interrupt  
- `OP[]` → Crash / Block  

---

## 📂 Project Structure / Unity Assets
Assets/
Animations/
Audio/
Scripts/
Sprites/
Video/ (See note below – videos are local only)
Library/
ProjectSettings/
Packages/

### **Video Assets (Important Note)**  
Our game uses several large background videos, menu loops, and animated stage elements.  
Examples include:

- Coldplay_MainMenu.mp4  
- MusicBG_H264.mp4  
- DOP_Musicbackground_H264  
- Musicbackground.mp4  

These files range from **100MB to 450MB**, which exceeds GitHub’s maximum file size limit of **100MB per file**.  
GitHub rejects pushes containing such large assets (even from past commits), so the team removed them from the repository and keeps them locally while Unity references them in the editor.

If needed, these videos can be shared privately outside of GitHub.

---

## ❌ Why Videos Cannot Be Uploaded to GitHub

GitHub imposes strict limits:

- **Maximum file size allowed:** 100 MB  
- Files larger than 100 MB are *blocked entirely*  
- Even previous commits containing large files prevent future pushes  
- Unity video assets often exceed 100–400MB  

To solve this, we cleaned the repository history and added a `.gitignore` rule:


This repository contains:

