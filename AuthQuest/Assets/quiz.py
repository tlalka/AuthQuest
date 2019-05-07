import json

def getJSON(filePathAndName):
    with open(filePathAndName, 'r') as fp:
        return json.load(fp)

myQuiz = getJSON('./save.json')

totalcount = 0
chest1choice = myQuiz.get("getItem")
chest1success = myQuiz.get("chestMethod")

q1choice = input("What method did you use to open the chest? Enter 1 for brute force"+
        " and 2 for solving the puzzle    ")

if(int(q1choice) == int(chest1success)):
    print("You are correct!")
    totalcount = totalcount + 1;
else:
    print("You are incorrect!")

q2choice = input("Did you succeed in opening the chest? Enter true or false     ")

if(q2choice == chest1choice):
    print("You are correct!")
    totalcount = totalcount + 1;
else:
    print("You are incorrect!")

print(str(totalcount) + " out of 2 correct")
