import json

def getJSON(filePathAndName):
    with open(filePathAndName, 'r') as fp:
        return json.load(fp)

myQuiz = getJSON('./save.json')

totalcount = 0
chest1choice = myQuiz.get("chest1choice")
chest1success = myQuiz.get("chest1success")
chest1item = myQuiz.get("chest1item")

q1choice = input("What method did you use to open the chest? Enter 1 for cracking"+
        "the chest and 2 for brute force    ")

if(int(q1choice) == int(chest1choice)):
    print("You are correct!")
    totalcount = totalcount + 1;
else:
    print("You are incorrect!")

q2choice = input("Did you succeed in opening the chest? Enter true or false     ")

if(q2choice == chest1success):
    print("You are correct!")
    totalcount = totalcount + 1;
else:
    print("You are incorrect!")

q3choice = input("What was the name of the weapon you received. Pick 1 for Dragon Scimitar, 2 for Armadyl Godsword, 3 for Abyssal Whip      ")

if(int(q3choice) == 2):
    print("You are correct!")
    totalcount = totalcount + 1;
else:
    print("You are incorrect!")

print(str(totalcount) + " out of 3 correct")
