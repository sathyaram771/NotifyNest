import supabase
import smtplib
from email.message import EmailMessage
from flask import Flask, render_template, request,current_app,redirect,url_for,session

lis = [1]

supabase_url = 'https://zfudaaqvanjrrloxfeeg.supabase.co'
supabase_key = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InpmdWRhYXF2YW5qcnJsb3hmZWVnIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTU3Mjg5NDIsImV4cCI6MjAxMTMwNDk0Mn0.5A3CuXfOI_u6TQZOtJDPsxwLM9x2K8kMz6fKmJtkL0g'
supabase_client = supabase.Client(supabase_url, supabase_key)
app = Flask(__name__)
def send_email(subject, body, recipient_email, sender_email, sender_password):
    msg = EmailMessage()
    msg.set_content(body)
    msg['Subject'] = subject
    msg['From'] = sender_email
    msg['To'] = recipient_email

    try:
        with smtplib.SMTP_SSL('smtp.gmail.com', 465) as smtp:
            smtp.login(sender_email, sender_password)
            smtp.send_message(msg)
        print("Email sent successfully!")
    except Exception as e:
        print(f"Error sending email: {e}")


@app.route('/editpage')
def editpage():
    query = supabase_client.table('details').select("*").execute()
    d = query.data
    l = 1
    data = []
    for i in d:
        b = []
        b.append(i['name'])
        b.append(i['class'])
        b.append(i['section'])
        b.append(i['roll_no'])
        b.append(i['email'])
        b.append(lis[l])
        l+=1
        data.append(b)
    return render_template('editpage.html',data = data)

@app.route('/edit',methods=['POST'])
def edit():
    query = supabase_client.table('details').select("*").execute()
    d = query.data
    if request.method == "POST":
        name = request.form['name']
        roll_no = request.form['roll_no']
        response1 = supabase_client.table('details').select('*').eq('roll_no', roll_no)
        #data1 = response1.data
        response2 = supabase_client.table('details').select('*').eq('name',name)
        #data2 = response2.data
        ans1 = response1.execute()
        ans2 = response2.execute()
        data = []
        for i in ans1.data:
            if i in ans2.data:
                data.append(i)
        print(ans1)
        print(ans2)
        print(data)
        if data:
            query = supabase_client.table('details').select("*").execute()
            d = query.data
            b = d.index(data[0])
            lis[b+1] = request.form['curr_att']
            return redirect('/finalattendence')
        else:
            sregis = "There is no student named as your input"
            query = supabase_client.table('details').select("*").execute()
            d = query.data
            l = 1
            data = []
            for i in d:
                b = []
                b.append(i['name'])
                b.append(i['class'])
                b.append(i['section'])
                b.append(i['roll_no'])
                b.append(i['email'])
                b.append(lis[l])
                l+=1
                data.append(b)
            return render_template('editpage.html',sregis = sregis,data = data)
    return render_template('editpage.html')

        


    

@app.route('/sendmessage')
def sendmessage():
    k = 1
    query = supabase_client.table('details').select("*").execute()
    d = query.data
    for j in range(1,len(lis)):
        send_email("REGARDING TODAY'S ATTENDENCE","YOUR CHILD "+ d[j-1]['name']+" is "+str(lis[k])+" today ", d[j-1]['email'], "sathyaalh3@gmail.com", "ciwrjdwacdmauego")
        k+=1
    return 'email send successfully'

@app.route('/login',methods=['POST','GET'])
def login():
    if request.method=="POST":
        id = request.form['id']
        password = request.form['password']
        res1 = supabase_client.table('admin').select('*').eq('id', id)
        res2 = supabase_client.table('admin').select('*').eq('password', password)
        ans1 = res1.execute()
        ans2 = res2.execute()
        data = []
        for i in ans1.data:
            if i in ans2.data:
                data.append(i)
        if data:
            return redirect('/home')
        else:
            d = "INVALID CREDENTIALS"
            return render_template('login.html',d=d)
    else:
        return render_template('login.html')

@app.route('/')
def hai():
    return render_template('login.html')


    

@app.route('/home')
def list():
    query = supabase_client.table('details').select("*").execute()
    d = query.data
    print(d)
    return render_template('list.html',data = d[0])

@app.route('/store',methods = ['GET','POST'])
def store():
    query = supabase_client.table('details').select("*").execute()
    d = query.data
    a = request.form.get('attendance')
    lis.append(a)
    print(a)
    lis[0]+=1
    if(lis[0]-1<len(d)):
        return render_template('list.html',data = d[lis[0]-1])
    else:
        return redirect('/finalattendence')

@app.route('/finalattendence')
def fad():
    query = supabase_client.table('details').select("*").execute()
    d = query.data
    l = 1
    data = []
    for i in d:
        b = []
        b.append(i['name'])
        b.append(i['class'])
        b.append(i['section'])
        b.append(i['roll_no'])
        b.append(i['email'])
        b.append(lis[l])
        l+=1
        data.append(b)
        print(data)
    return render_template('finallist.html',data = data)



#data = {'roll_no': 12345679,'name': 'kishore', 'class': 'XII','section': 'A','email': 'sathyaalh3@gmail.com','password':'124'}
#upsert_query = supabase_client.table('details').upsert([data])

#query = supabase_client.table('details').select("*").execute()
#d = query.data
#for i in d:
    #send_email("Attendence regarding","your son "+ i['name'] + " of class "+ i['class'] +" is present today ",i['email'], "sathyaalh3@gmail.com", "ciwrjdwacdmauego")
    #print(i)
#print(d)

if __name__ == '__main__':
    app.run(debug=True)

    




