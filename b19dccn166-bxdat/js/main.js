$(document).ready(function () {
  function showModal(modifier, title) {
    // Xử lý tạo modal và kiểm tra modal chưa có trong DOM mới thực thi
    if (document.querySelector(".modal") == null) {
      const modalWarning = `<div class="modal-wrapper">
      <div class="modal-top"><h2>${title}</h2></div>
      <div class="modal-content">
        <i class="fas fa-exclamation-triangle"></i>
        <span>Bạn có chắc muốn xóa nhân viên này không?</span>
      </div>
    </div>`;
      const modalPrimary = `<div class="modal-wrapper">
      <div class="modal-top"><h2>${title}</h2></div>
      <div class="modal-main">
        <div class="modal-content__left">
          <a href="#!" class="modal-content__left-avatar">
            <img src="./img/default-avatar.jpg" alt="this-is-avatar" />
          </a>
          <span
            >Vui lòng chọn ảnh có định dạng (.jpg, .jpeg, .png, .gif.)</span
          >
        </div>
        <div class="modal-content__right">
          <div class="info">
            <span class="info-title">A. Thông tin chung.</span>
            <div class="info-form">
              <div class="info-row">
                <div class="info-row__column">
                  <span>Mã nhân viên (<span>*</span>)</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                  </div>
                </div>
                <div class="info-row__column">
                  <span>Họ và tên (<span>*</span>)</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                  </div>
                </div>
              </div>
              <div class="info-row">
                <div class="info-row__column">
                  <span>Ngày sinh</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                    <i class="far fa-calendar"></i>
                  </div>
                </div>
                <div class="info-row__column">
                  <span>Giới tính</span>
                  <select class="input-row__column-select" name="" id="">
                    <option value="">Nam</option>
                    <option value="">Nữ</option>
                    <option value="">Khác</option>
                  </select>
                </div>
              </div>
              <div class="info-row">
                <div class="info-row__column">
                  <span>Số CMTND/Căn cước (<span>*</span>)</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                  </div>
                </div>
                <div class="info-row__column">
                  <span>Ngày cấp</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                    <i class="far fa-calendar"></i>
                  </div>
                </div>
              </div>
              <div class="info-row">
                <div class="info-row__column">
                  <span>Nơi cấp</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                  </div>
                </div>
                <div class="info-row__column"></div>
              </div>
              <div class="info-row">
                <div class="info-row__column">
                  <span>Email (<span>*</span>)</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                  </div>
                </div>
                <div class="info-row__column">
                  <span>Số điện thoại (<span>*</span>)</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                  </div>
                </div>
              </div>
            </div>
            <span class="info-title info-title--mt">Thông tin công việc</span>
            <div class="info-form">
              <div class="info-row">
                <div class="info-row__column">
                  <span>Vị trí</span>
                  <select class="input-row__column-select" name="" id="">
                    <option value="">Giám đốc</option>
                    <option value="">Demo 2</option>
                    <option value="">Demo 3</option>
                  </select>
                </div>
                <div class="info-row__column">
                  <span>Phòng ban</span>
                  <select class="input-row__column-select" name="" id="">
                    <option value="">Phòng nhân sự</option>
                    <option value="">Nữ</option>
                    <option value="">Khác</option>
                  </select>
                </div>
              </div>
              <div class="info-row">
                <div class="info-row__column">
                  <span>Mã số thuế cá nhân</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                  </div>
                </div>
                <div class="info-row__column">
                  <span>Mức lương cơ bản</span>
                  <div class="input-row__column-input">
                    <input type="text" placeholder="VNĐ" />
                  </div>
                </div>
              </div>
              <div class="info-row">
                <div class="info-row__column">
                  <span>Ngày sinh</span>
                  <div class="input-row__column-input">
                    <input type="text" />
                    <i class="far fa-calendar"></i>
                  </div>
                </div>
                <div class="info-row__column">
                  <span>Tình trạng công việc</span>
                  <select class="input-row__column-select" name="" id="">
                    <option value="">Đang làm việc</option>
                    <option value="">Demo 2</option>
                    <option value="">Demo 3</option>
                  </select>
                </div>
              </div>
            </div>
              </div>
            </div>
          </div>
      </div> `;
      const modalPrimaryBottom = `<div class="modal-bottom">
    <button class="modal-bottom__close">Huỷ</button><button class="modal-bottom__remove">
      <i class="far fa-save"></i> Lưu
    </button>
  </div>`;
      const modalWarningBottom = `<div class="modal-bottom">
    <button class="modal-bottom__close">Huỷ</button><button class="modal-bottom__remove">
      Xóa
    </button>
  </div>`;
      const modalContent = `
          <div class="modal modal--${modifier}">
          <img class="modal-close" src="./icon/x.svg" alt="this-is-icon" />
          ${modifier == "primary" ? modalPrimary : modalWarning}
        ${modifier == "primary" ? modalPrimaryBottom : modalWarningBottom} 
        </div>
          `;
      const overlay = `<div id="overlay"</div>`;
      $("body").append(modalContent, overlay);
      $(".modal").addClass("show");
      $("#overlay").addClass("show");
    }
  }

  // Xử lý ẩn modal và overlay
  function hideModal() {
    $(".modal").remove();
    $("#overlay").remove();
  }

  // Xử lý khi bấm vào nút thêm nhân viên
  $(".content-member__add").on("click", function () {
    showModal("primary", "Thông tin nhân viên");
  });

  // Xử lý khi bấm vào nút đóng modal
  $(document).on("click", ".modal-close, .modal-bottom__close", function () {
    hideModal();
  });

  // Xử lý khi hover vào tr
  let tableRows = document.querySelectorAll(".table-body table tr");

  tableRows.forEach((row) => {
    row.addEventListener("mouseenter", function () {
      let deleteIconDiv = document.createElement("div");
      deleteIconDiv.innerHTML = ` <div class="edit-tr">
    <i class="edit-tr__remove fas fa-trash"></i>
    <i class="edit-tr__change fas fa-pen"></i>
  </div>`;
      deleteIconDiv.style.position = "absolute";
      deleteIconDiv.style.right = "1rem";
      deleteIconDiv.style.top = "50%";
      deleteIconDiv.style.transform = "translateY(-50%)";
      this.appendChild(deleteIconDiv);
    });

    row.addEventListener("mouseleave", function () {
      this.removeChild(this.lastChild);
    });
  });

  // Xử lý khi click vào nút edit sửa xóa
  $(document).on("click", ".edit-tr__change", function () {
    showModal("primary", "Sửa thông tin nhân viên");
  });
  $(document).on("click", ".edit-tr__remove", function () {
    showModal("warning", "Pop-up thông báo");
  });
});
