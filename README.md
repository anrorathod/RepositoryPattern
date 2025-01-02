# RepositoryPattern
C# Repository Pattern Demo Application for .Net Core 7

<h3>TEST API</h3>
<ul>
  <li>Get User Token</li>
</ul>

# Test API
<h3>Get User Token</h3>
<b>Request</b><br>
<b>Method:</b> POST<br>
<b>URL:</b> {{baseurl}}/User/token?Username=admin@demoapp.com&Password=demo&utype=admin
<br>
<b>Response</b>
<pre>
{
    "success": true,
    "message": null,
    "exceptions": null,
    "recordsTotal": 0,
    "recordsFiltered": 0,
    "data": {
        "username": "demo",
        "email": "admin@demoapp.com",
        "token": "ey*****CJ9.***********.HU56NMsPVBXp--27J******Zec"
    }
}
</pre>

<h3>Add Country</h3>
<b>Request</b><br>
<b>Method:</b> POST<br>
<b>URL:</b> {{baseurl}}/Country/create<br>
<b>Reqeust Body:</b> Json type
<pre>
  {   
  "countryName": "Japan",
  "countryCode": "JP",
  "countryPhoneCode": "11",
   "description": 'Japan'
}
</pre>
<br></b>
<b>Response 1</b>
<pre>
{
    "success": true,
    "message": null,
    "exceptions": null,
    "recordsTotal": 0,
    "recordsFiltered": 0,
    "data": true
}}
</pre>
<b>Response 2</b>
<pre>
{
    "success": false,
    "message": "Country name or Country Code already exists.",
    "exceptions": null,
    "recordsTotal": 0,
    "recordsFiltered": 0,
    "data": false
}
</pre>
