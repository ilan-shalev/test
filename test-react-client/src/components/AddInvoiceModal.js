import React, { useState } from "react";
import { Button, Input, Modal, List, Typography } from "antd";
import { axiosInstance } from "../axios";
import styled from "styled-components";
import ProductInput from "./ProductInput";

export default function AddInvoiceModal({ show, onFinish, onCancel }) {
  const [invoice, setInvoice] = useState({});
  const [showAddProduct, setShowAddProduct] = useState(false);

  const addInvoice = () =>
    axiosInstance.post("/invoices", invoice).then((res) => {
      setInvoice({});
      setShowAddProduct(false);
      onFinish(res.data);
    });

  return (
    <ModalForm
      open={show}
      onCancel={() => {
        setInvoice({});
        setShowAddProduct(false);
        onCancel();
      }}
      onOk={addInvoice}
    >
      <Input
        placeholder="Recipient"
        value={invoice.recipient}
        onChange={(e) =>
          setInvoice((prev) => {
            return { ...prev, recipient: e.target.value };
          })
        }
      />
      <List
        header={<div>products</div>}
        // footer={<div>Footer</div>}
        bordered
        dataSource={invoice.products}
        renderItem={(product) => (
          <List.Item>
            <Typography.Text strong> {product.name} </Typography.Text>
            {product.price}
            {product.quantity}
          </List.Item>
        )}
      />
      {showAddProduct && (
        <ProductInput
          onFinish={(product) => {
            setShowAddProduct(false);
            setInvoice((prev) => {
              return { ...prev, products: [...(prev.products ?? []), product] };
            });
          }}
        />
      )}
      {!showAddProduct && (
        <Button onClick={() => setShowAddProduct(true)}>Add Product</Button>
      )}
    </ModalForm>
  );
}

const ModalForm = styled(Modal)`
  display: flex;
  flex-direction: column;
  gap: 4rem;
`;
