import { Button, Table, Space } from "antd";
import { useState, useEffect } from "react";
import { styled } from "styled-components";
import AddInvoiceModal from "./components/AddInvoiceModal";
import EditInvoiceModal from "./components/EditInvoiceModal";
import { axiosInstance } from "./axios";

export default function App() {
  const [invoices, setInvoices] = useState([]);
  const [showAddInvoiceModal, setShowAddInvoiceModal] = useState(false);
  const [edit, setEdit] = useState(null);

  useEffect(() => {
    axiosInstance.post("all", {}).then((res) => setInvoices(res.data));
  }, []);

  const columns = [
    {
      title: "recipient",
      dataIndex: "recipient",
      key: "recipient",
    },
    {
      title: "time Of Purchase",
      dataIndex: "timeOfPurchase",
      key: "timeOfPurchase",
    },
    {
      title: "Action",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <Button type="link" onClick={() => setEdit(record)}>
            Edit
          </Button>
          <Button
            type="dashed"
            danger
            onClick={() =>
              axiosInstance
                .delete(`/invoices/${record.id}`, record)
                .then((res) =>
                  setInvoices((prev) => prev.filter((x) => x.id !== record.id))
                )
            }
          >
            Delete
          </Button>
        </Space>
      ),
    },
  ];

  return (
    <Wrapper>
      <Button
        type="primary"
        onClick={() => setShowAddInvoiceModal((prev) => !prev)}
      >
        Add Recipt
      </Button>
      <Table dataSource={invoices} columns={columns} />
      <AddInvoiceModal
        show={showAddInvoiceModal}
        onFinish={(invoice) => {
          setShowAddInvoiceModal(false);
          setInvoices((prev) => [invoice, ...prev]);
        }}
        onCancel={() => setShowAddInvoiceModal(false)}
      />
      {edit !== null && (
        <EditInvoiceModal
          show
          invoice={edit}
          onFinish={(editedInvoice) => {
            setEdit(null);
            axiosInstance
              .put("/invoices", editedInvoice)
              .then(() => setInvoices((prev) => [...prev.filter(x => x.id !== editedInvoice.id), editedInvoice]));
          }}
          onCancel={() => setEdit(null)}
        />
      )}
    </Wrapper>
  );
}

const Wrapper = styled.div`
  text-align: center;
  padding: 20px;
  display: flex;
  flex-direction: column;
  gap: 5rem;
  margin-left: 25%;
  margin-right: 25%;
`;
