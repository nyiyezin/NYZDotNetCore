let blogId = null;

getBlogTable();
// readBlog()
// createBlog()
// updateBlog("57d02def-edbb-4985-ab77-b27c2c0638ef","title","author", "content");
// deleteBlog("1476fc91-022b-4774-96ff-d5cbb180dfdc");
function readBlog() {
  let lst = getBlogs();
  console.log(lst);
}

function createBlog(title, author, content) {
  let lst = getBlogs();

  const requestModel = {
    id: crypto.randomUUID(),
    title: title,
    author: author,
    content: content,
  };
  lst.push(requestModel);
  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(blogs, jsonBlog);
  successMessage("Saving success.");
  clearControls();
}

function editBlog(id) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  console.log(items);

  if (items.length == 0) {
    console.log("no data was found.");
    errorMessage("No data was found.");
    return;
  }
  let item = items[0];
  blogId = item.id;
  $("#txtTitle").val(item.title);
  $("#txtAuthor").val(item.author);
  $("#txtContent").val(item.content);
  $("#txtTitle").focus();
}

function updateBlog(id, title, author, content) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  console.log(items);

  if (items.length == 0) {
    console.log("no data was found.");
    errorMessage("No data was found.");
    return;
  }
  const item = items[0];
  item.title = title;
  item.author = author;
  item.content = content;

  const index = lst.findIndex((x) => x.id === id);
  lst[index] = item;

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);
  successMessage("Updating successful.");
}

function deleteBlog(id) {
  let result = confirm("Are you sure to delete?");
  if (!result) return;
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  if (items.length == 0) {
    console.log("no data was found.");
    return;
  }
  lst = lst.filter((x) => x.id !== id);

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);

  successMessage("Deleting success.");
  getBlogTable();
}

function getBlogs() {
  const blogs = localStorage.getItem(tblBlog);
  console.log(blogs);

  let lst = [];
  if (blogs !== null) {
    lst = JSON.parse(blogs);
  }
  return lst;
}

$("#btnSave").click(function () {
  const title = $("#txtTitle").val();
  const author = $("#txtAuthor").val();
  const content = $("#txtContent").val();

  if (blogId === null) {
    createBlog(title, author, content);
  } else {
    updateBlog(blogId, title, author, content);
    blogId = null;
  }
  getBlogTable();
});

function successMessage(message) {
  alert(message);
}
function errorMessage(message) {
  alert(message);
}

function clearControls() {
  $("#txtTitle").val("");
  $("#txtAuthor").val("");
  $("#txtContent").val("");
  $("#txtTitle").focus();
}

function getBlogTable() {
  const lst = getBlogs();
  let count = 0;
  let htmlRows = "";
  lst.forEach((item) => {
    const htmlRow = `<tr>
            <td>
            <button type="button" class="btn btn-warning" onclick="editBlog('${
              item.id
            }')">Edit</button>
            <button type="button" class="btn btn-danger" onclick="deleteBlog('${
              item.id
            }')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>
    `;
    htmlRows += htmlRow;
  });
  $("#tbody").html(htmlRows);
}
